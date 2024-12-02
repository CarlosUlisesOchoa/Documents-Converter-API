using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DotNetEnv;
using DocumentsConverter.Models;
using Microsoft.AspNetCore.Mvc;
using DocumentsConverter.Services.Interfaces;
using DocumentsConverter.Services;
using DocumentsConverter.Utilities;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json.Serialization;

Env.Load(); // This loads variables from the .env file into the environment

var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
var jwksUrl = Environment.GetEnvironmentVariable("JWKS_URL");

// Validate that the variables are set and not empty
if (string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience) || string.IsNullOrEmpty(jwksUrl))
{
    Console.WriteLine("Error: The environment variables JWT_ISSUER, JWT_AUDIENCE, and JWKS_URL must be set in the .env file.");
    Console.WriteLine("Please ensure you have renamed the .env.example file to .env and configured the variables properly.");
    Environment.Exit(1);
}

var builder = WebApplication.CreateBuilder(args);

// Register the custom ProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Preserve the Pascal case
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        // Optional: Handle reference loops if any
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var problemDetails = new ExtendedProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid Input",
                Detail = "One or more validation errors occurred.",
                Errors = context.ModelState
                            .Where(e => e.Value.Errors.Count > 0)
                            .SelectMany(e => e.Value.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToArray()
            };

            return new BadRequestObjectResult(problemDetails)
            {
                ContentTypes = { "application/json" }
            };
        };
    });

// Register DocumentConverterService
builder.Services.AddTransient<IDocumentConverterService, DocumentConverterService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Include your JWT token in the following format: Bearer {token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Add Authentication services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
            {
                var client = new HttpClient();
                var json = client.GetStringAsync(jwksUrl).Result;
                var keys = new JsonWebKeySet(json).Keys;
                return keys;
            }
        };
        // Handle 401 Unauthorized using ProblemDetails
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();

                var problemDetails = new ExtendedProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Detail = "Access token is expired or invalid",
                };

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsJsonAsync(problemDetails);
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();