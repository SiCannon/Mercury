using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    public class Recording
    {
        [XmlAttribute("id")]
        public Guid RecordingId { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("length")]
        public int Length { get; set; }

        [XmlArray("isrc-list")]
        [XmlArrayItem("isrc")]
        public List<Isrc> Isrcs { get; set; }

        [XmlArray("relation-list")]
        [XmlArrayItem("relation")]
        public RelationList Relations { get; set; }
    }
}
