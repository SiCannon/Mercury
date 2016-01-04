using System.Collections.Generic;
using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    public class RelationList : List<Relation>
    {
        [XmlAttribute("target-type")]
        public string TargetType { get; set; }
    }
}
