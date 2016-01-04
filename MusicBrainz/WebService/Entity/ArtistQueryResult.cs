using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    [XmlRoot("metadata")]
    public class ArtistQueryResult
    {
        [XmlElement("artist")]
        public Artist Artist { get; set; }
    }
}
