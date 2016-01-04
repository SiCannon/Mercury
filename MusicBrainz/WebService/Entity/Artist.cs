using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    public class Artist
    {
        [XmlAttribute("id")]
        public Guid ArtistId { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("sort-name")]
        public string SortName { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("score", Namespace = "http://musicbrainz.org/ns/ext#-2.0")]
        public int Score { get; set; }

        [XmlArray("alias-list")]
        [XmlArrayItem("alias")]
        public List<string> Aliases { get; set; }

        [XmlElement("ipi")]
        public string Ipi { get; set; }

        [XmlArray("release-group-list")]
        [XmlArrayItem("release-group")]
        public List<ReleaseGroup> ReleaseGroups { get; set; }

        [XmlArray("tag-list")]
        [XmlArrayItem("tag")]
        public List<Tag> Tags { get; set; }
    }
}
