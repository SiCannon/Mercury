using System;
using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    public class Relation
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlElement("target")]
        public Guid Target { get; set; }
    }
}
