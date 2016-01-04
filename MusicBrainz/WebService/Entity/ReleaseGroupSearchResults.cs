using System.Collections.Generic;
using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    [XmlRoot("metadata")]
    public class ReleaseGroupSearchResults
    {
        [XmlArray("release-group-list")]
        [XmlArrayItem("release-group")]
        public List<ReleaseGroup> ReleaseGroups { get; set; }
    }
}
