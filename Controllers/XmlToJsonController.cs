using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace XMLToJSONConverter.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize] // Enforces authorization on all actions in this controller
    public class XmlToJsonController : ControllerBase
    {
        [HttpPost("transform")]
        public IActionResult Post([FromBody] string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return CustomProblemDetailsResponse(
                    status: StatusCodes.Status400BadRequest,
                    title: "Invalid Input",
                    detail: "The provided input is null, empty, or consists only of whitespace."
                );
            }

            try
            {
                // Process to parse XML to JSON here...

                // Solution goes here...

                // Final step
                var response = new
                {
                    Emisor = new { },
                    Receptor = new { },
                    Conceptos = Array.Empty<object>(),
                    Impuestos = new { }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return CustomProblemDetailsResponse(
                    status: StatusCodes.Status500InternalServerError, 
                    title: "Internal Server Error",
                    //detail: $"An unexpected error occurred: {ex.Message}", // Not sure if send "ex.Message" to client or just log it internally
                    detail: "An unexpected error occurred.", 
                    errors: ["Contact support if this error persists."]
                );
            }
        }

        /// <summary>
        /// Creates a standardized extended ProblemDetails response.
        /// </summary>
        private IActionResult CustomProblemDetailsResponse(int status, string title, string detail, string[]? errors = null)
        {
            var problemDetails = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = detail
            };

            if (errors != null && errors.Length > 0)
            {
                problemDetails.Extensions.Add("errors", errors);
            }

            return new ObjectResult(problemDetails)
            {
                StatusCode = status
            };
        }
    }
}