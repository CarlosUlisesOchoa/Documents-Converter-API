using DocumentsConverter.Models;

namespace DocumentsConverter.Services.Interfaces
{
    public interface IDocumentConverterService
    {
        ComprobanteWrapper ConvertXmlToJson(XmlRequest request);
    }
}
