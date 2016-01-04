using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    public class TextRepresentation
    {
        [XmlElement("language")]
        public string Language { get; set; }

        [XmlElement("script")]
        public string Script { get; set; }
    }
}
