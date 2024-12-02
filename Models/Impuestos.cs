using System.Xml.Serialization;

namespace DocumentsConverter.Models
{
    public class Impuestos
    {
        [XmlAttribute("TotalImpuestosRetenidos")]
        public string TotalImpuestosRetenidos { get; set; }

        [XmlAttribute("TotalImpuestosTrasladados")]
        public string TotalImpuestosTrasladados { get; set; }
    }
}
