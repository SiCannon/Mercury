using System.Collections.Generic;
using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    public class ArtistCredit
    {
        [XmlArray("name-credit")]
        [XmlArrayItem("artist")]
        public List<Artist> NameCredits { get; set; }
    }
}
