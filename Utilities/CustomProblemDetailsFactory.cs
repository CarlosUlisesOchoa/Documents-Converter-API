using DocumentsConverter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DocumentsConverter.Utilities
{
    public class CustomProblemDetailsFactory : ProblemDetailsFactory
    {
        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext,
            int? statusCode = null,
            string title = null,
            string type = null,
            string detail = null,
            string instance = null)
        {
            var status = statusCode ?? 500;
            var problemDetails = new ExtendedProblemDetails
            {
                Status = status,
                Title = title ?? ResolveStatusCodeTitle(status),
                Detail = detail ?? ResolveStatusCodeDetail(status),
                Errors = Array.Empty<string>()
            };

            return problemDetails;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext,
            ModelStateDictionary modelStateDictionary,
            int? statusCode = null,
            string title = null,
            string type = null,
            string detail = null,
            string instance = null)
        {
            var status = statusCode ?? StatusCodes.Status400BadRequest;

            var problemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = status,
                Title = title ?? "Invalid Input",
                Detail = detail ?? "One or more validation errors occurred.",
                Instance = instance,
                Type = type
            };

            // Optionally, you can manipulate the Errors dictionary if needed
            // For example, flattening the errors into a single array
            var errors = problemDetails.Errors.SelectMany(kvp => kvp.Value).ToArray();
            problemDetails.Extensions.Add("errors", errors);

            return problemDetails;
        }
        // Helper methods to resolve status code titles and details
        private static string ResolveStatusCodeTitle(int statusCode) => statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            415 => "Unsupported Media Type",
            500 => "Internal Server Error",
            _ => "Error"
        };

        private static string ResolveStatusCodeDetail(int statusCode) => statusCode switch
        {
            400 => "The request was invalid.",
            401 => "Access token is expired or invalid",
            403 => "You don't have permission to access this resource.",
            404 => "The requested resource was not found.",
            415 => "The request content type is not supported.",
            500 => "An unexpected error occurred.",
            _ => "An error occurred processing your request."
        };
    }
}
