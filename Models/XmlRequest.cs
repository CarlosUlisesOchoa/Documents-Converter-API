using System.ComponentModel.DataAnnotations;

namespace XMLToJSONConverter.Models
{
    public class XmlRequest
    {
        [Required]
        public string Xml { get; set; }
    }
}
