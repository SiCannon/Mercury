using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    public class Tag
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlAttribute("count")]
        public int Count { get; set; }
    }
}
