using System.Xml.Serialization;

namespace DocumentsConverter.Models
{
    public class Concepto
    {
        [XmlAttribute("NoIdentificacion")]
        public string NoIdentificacion { get; set; }

        [XmlAttribute("ClaveProdServ")]
        public string ClaveProdServ { get; set; }

        [XmlAttribute("Descripcion")]
        public string Descripcion { get; set; }

        [XmlAttribute("ClaveUnidad")]
        public string ClaveUnidad { get; set; }

        [XmlAttribute("ValorUnitario")]
        public string ValorUnitario { get; set; }

        [XmlAttribute("Cantidad")]
        public string Cantidad { get; set; }

        [XmlAttribute("Importe")]
        public string Importe { get; set; }

        [XmlAttribute("ObjetoImp")]
        public string ObjetoImp { get; set; }
    }
}
