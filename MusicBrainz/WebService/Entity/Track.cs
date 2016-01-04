using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    public class Track
    {
        [XmlElement("position")]
        public int Position { get; set; }

        [XmlElement("number")]
        public string Number { get; set; }

        [XmlElement("length")]
        public int Length { get; set; }

        [XmlElement("recording")]
        public Recording Recording { get; set; }
    }
}
