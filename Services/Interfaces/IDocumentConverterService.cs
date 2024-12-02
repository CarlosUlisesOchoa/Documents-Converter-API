using DocumentsConverter.Models;

namespace DocumentsConverter.Services.Interfaces
{
    public interface IDocumentConverterService
    {
        object ConvertXmlToJson(XmlRequest request);
    }
}
