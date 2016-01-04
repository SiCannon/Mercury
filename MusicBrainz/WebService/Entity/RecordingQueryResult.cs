using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    [XmlRoot("metadata")]
    public class RecordingQueryResult
    {
        [XmlElement("recording")]
        public Recording Recording { get; set; }
    }
}
