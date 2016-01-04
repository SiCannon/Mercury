using System;
using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    public class Work
    {
        [XmlAttribute("id")]
        public Guid SongId { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("iswc")]
        public string Iswc { get; set; }

        [XmlArray("relation-list")]
        [XmlArrayItem("relation")]
        public RelationList Relations { get; set; }
    }
}
