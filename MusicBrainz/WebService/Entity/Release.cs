using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    public class Release
    {
        [XmlAttribute("id")]
        public Guid ReleaseId { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("quality")]
        public string Quality { get; set; }

        [XmlElement("text-representation")]
        public TextRepresentation TextRepresentation { get; set; }

        [XmlElement("date")]
        public string Date { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlElement("barcode")]
        public string Barcode { get; set; }

        [XmlElement("asin")]
        public string Asin { get; set; }

        [XmlArray("medium-list")]
        [XmlArrayItem("medium")]
        public List<Medium> Mediums { get; set; }
    }
}
