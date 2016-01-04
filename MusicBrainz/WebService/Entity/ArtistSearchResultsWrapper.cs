using System.Collections.Generic;
using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    [XmlRoot("metadata")]
    public class ArtistSearchResultWrapper
    {
        [XmlArray("artist-list")]
        [XmlArrayItem("artist")]
        public List<Artist> Artists { get; set; }
    }
}
