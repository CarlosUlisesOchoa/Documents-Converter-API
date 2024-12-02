using Microsoft.AspNetCore.Mvc;

namespace XMLToJSONConverter.Models
{
    public class ExtendedProblemDetails : ProblemDetails
    {
        public string[] Errors { get; set; } = Array.Empty<string>(); // Initialize with an empty array
    }
}
