using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    [XmlRoot("metadata")]
    public class WorkQueryResult
    {
        [XmlElement("work")]
        public Work Work { get; set; }
    }
}
