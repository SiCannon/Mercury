using System.Collections.Generic;
using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    public class Medium
    {
        [XmlElement("position")]
        public int Position { get; set; }

        [XmlElement("format")]
        public string Format { get; set; }

        [XmlArray("track-list")]
        [XmlArrayItem("track")]
        public List<Track> Tracks { get; set; }
    }
}
