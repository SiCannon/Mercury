using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    public class Isrc
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
    }
}
