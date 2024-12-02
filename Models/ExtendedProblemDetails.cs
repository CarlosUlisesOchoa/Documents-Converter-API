using Microsoft.AspNetCore.Mvc;

namespace DocumentsConverter.Models
{
    public class ExtendedProblemDetails : ProblemDetails
    {
        public string[] Errors { get; set; } = Array.Empty<string>(); // Initialize with an empty array
    }
}
