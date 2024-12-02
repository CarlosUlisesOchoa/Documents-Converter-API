using System.Xml.Serialization;

namespace DocumentsConverter.Models
{
    [XmlRoot("Comprobante")]
    public class Comprobante
    {
        [XmlAttribute("Version")]
        public string Version { get; set; }

        [XmlAttribute("LugarExpedicion")]
        public string LugarExpedicion { get; set; }

        [XmlAttribute("MetodoPago")]
        public string MetodoPago { get; set; }

        [XmlAttribute("FormaPago")]
        public string FormaPago { get; set; }

        [XmlAttribute("TipoDeComprobante")]
        public string TipoDeComprobante { get; set; }

        [XmlAttribute("Folio")]
        public string Folio { get; set; }

        [XmlAttribute("Moneda")]
        public string Moneda { get; set; }

        [XmlAttribute("Serie")]
        public string Serie { get; set; }

        [XmlAttribute("Fecha")]
        public string Fecha { get; set; }

        [XmlAttribute("Total")]
        public string Total { get; set; }

        [XmlAttribute("SubTotal")]
        public string SubTotal { get; set; }

        [XmlAttribute("UUID")]
        public string UUID { get; set; }

        [XmlElement("Emisor")]
        public Emisor Emisor { get; set; }

        [XmlElement("Receptor")]
        public Receptor Receptor { get; set; }

        [XmlArray("Conceptos")]
        [XmlArrayItem("Concepto")]
        public List<Concepto> Conceptos { get; set; }

        [XmlElement("Impuestos")]
        public Impuestos Impuestos { get; set; }
    }
}
