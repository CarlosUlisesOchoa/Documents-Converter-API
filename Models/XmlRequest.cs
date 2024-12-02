using System.ComponentModel.DataAnnotations;

namespace DocumentsConverter.Models
{
    public class XmlRequest
    {
        [Required]
        public string Xml { get; set; }
    }
}
