using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DocumentsConverter.Models;
using DocumentsConverter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DocumentsConverter.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize] // Enforces authorization on all actions in this controller
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentConverterService _documentConverterService;

        public DocumentsController(IDocumentConverterService documentConverterService)
        {
            _documentConverterService = documentConverterService;
        }

        [HttpPost("xml-to-json")]
        public IActionResult Post([FromBody] XmlRequest req)
        {
            try
            {
                var result = _documentConverterService.ConvertXmlToJson(req);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                var problemDetails = HttpContext.RequestServices
                    .GetRequiredService<ProblemDetailsFactory>()
                    .CreateProblemDetails(HttpContext, StatusCodes.Status400BadRequest, ex.Message);

                return new ObjectResult(problemDetails)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            catch (InvalidOperationException ex)
            {
                var problemDetails = HttpContext.RequestServices
                    .GetRequiredService<ProblemDetailsFactory>()
                    .CreateProblemDetails(HttpContext, StatusCodes.Status500InternalServerError, ex.Message);

                return new ObjectResult(problemDetails)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}