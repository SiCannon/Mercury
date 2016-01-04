using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    [XmlRoot("metadata")]
    public class ReleaseQueryResult
    {
        [XmlElement("release")]
        public Release Release { get; set; }
    }
}
