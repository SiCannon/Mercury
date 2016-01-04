using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    [XmlRoot("metadata")]
    public class ReleaseGroupQueryResult
    {
        [XmlElement("release-group")]
        public ReleaseGroup ReleaseGroup { get; set; }
    }
}
