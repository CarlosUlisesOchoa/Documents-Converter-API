using DocumentsConverter.Models;
using DocumentsConverter.Services.Interfaces;

namespace DocumentsConverter.Services
{
    public class DocumentConverterService : IDocumentConverterService
    {
        public object ConvertXmlToJson(XmlRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Xml))
            {
                throw new ArgumentException("The provided input is null, empty, or consists only of whitespace.");
            }

            try
            {
                // Process to parse XML to JSON here...

                // Solution goes here...

                // Final step

                // Transform to desired JSON structure (example response)
                var response = new
                {
                    Emisor = new { },
                    Receptor = new { },
                    Conceptos = Array.Empty<object>(),
                    Impuestos = new { }
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred.", ex);
            }
        }
    }
}
