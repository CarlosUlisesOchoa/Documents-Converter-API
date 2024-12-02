﻿using System.Xml.Serialization;

namespace DocumentsConverter.Models
{
    public class Receptor
    {
        [XmlAttribute("Nombre")]
        public string Nombre { get; set; }

        [XmlAttribute("Rfc")]
        public string Rfc { get; set; }
    }
}