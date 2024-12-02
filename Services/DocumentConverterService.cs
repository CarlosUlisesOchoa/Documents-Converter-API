using DocumentsConverter.Models;
using DocumentsConverter.Services.Interfaces;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DocumentsConverter.Services
{
    public class DocumentConverterService : IDocumentConverterService
    {
        private static readonly XmlSchemaSet _schemaSet;

        static DocumentConverterService()
        {
            try
            {
                _schemaSet = new XmlSchemaSet();
                var schemaPath = Path.Combine(AppContext.BaseDirectory, "Schemas", "comprobante.xsd");

                if (!File.Exists(schemaPath))
                {
                    throw new FileNotFoundException($"Schema file not found at: {schemaPath}");
                }

                using var schemaStream = File.OpenRead(schemaPath);
                var schema = XmlSchema.Read(schemaStream, null);
                _schemaSet.Add(schema);
                _schemaSet.Compile();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Schema loading error: {ex.Message}");
                throw;
            }
        }

        public ComprobanteWrapper ConvertXmlToJson(XmlRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Xml))
            {
                throw new ArgumentException("The provided input is null, empty, or consists only of whitespace.");
            }

            string xmlContent;
            try
            {
                byte[] xmlBytes = Convert.FromBase64String(request.Xml);
                xmlContent = Encoding.UTF8.GetString(xmlBytes);
            }
            catch (FormatException)
            {
                throw new ArgumentException("The provided XML is not a valid Base64 string.");
            }

            try
            {
                // Create XML document for validation
                var doc = new XmlDocument();
                doc.LoadXml(xmlContent);

                // Validate against schema
                var validationErrors = new List<string>();
                doc.Schemas = _schemaSet;
                doc.Validate((sender, e) =>
                {
                    validationErrors.Add(e.Message);
                });

                if (validationErrors.Any())
                {
                    throw new ArgumentException($"XML validation failed: {string.Join(", ", validationErrors)}");
                }

                // If validation passes, proceed with deserialization
                var xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

                var serializer = new XmlSerializer(typeof(Comprobante));

                using var stringReader = new StringReader(xmlContent);
                using var xmlReader = XmlReader.Create(stringReader);

                var comprobante = (Comprobante)serializer.Deserialize(xmlReader);
                return new ComprobanteWrapper { Comprobante = comprobante };
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"XML Processing Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw new InvalidOperationException($"Error processing XML: {ex.Message}", ex);
            }
        }
    }
}