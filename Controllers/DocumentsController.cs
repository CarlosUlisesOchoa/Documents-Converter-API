using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XMLToJSONConverter.Models;

namespace XMLToJSONConverter.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize] // Enforces authorization on all actions in this controller
    public class DocumentsController : ControllerBase
    {
        [HttpPost("xml-to-json")]
        public IActionResult Post([FromBody] XmlRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Xml))
            {
                return CustomProblemDetailsResponse(
                    status: StatusCodes.Status400BadRequest,
                    title: "Invalid Input",
                    detail: "The provided input is null, empty, or consists only of whitespace"
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
                    detail: "An unexpected error occurred",
                    errors: ["Please try again later", "If the issue persists, contact support with the error details"]
                );
            }
        }

        /// <summary>
        /// Creates a standardized extended ProblemDetails response.
        /// </summary>
        private IActionResult CustomProblemDetailsResponse(int status, string title, string detail, string[]? errors = null)
        {
            var problemDetails = new ExtendedProblemDetails
            {
                Status = status,
                Title = title,
                Detail = detail,
                Errors = errors ?? Array.Empty<string>()
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = status
            };
        }
    }
}