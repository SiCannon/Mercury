using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MusicBrainz.WebService.Entity
{
    public class ReleaseGroup
    {
        [XmlAttribute("id")]
        public Guid ReleaseGroupId { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("score", Namespace = "http://musicbrainz.org/ns/ext#-2.0")]
        public int Score { get; set; }

        [XmlElement("primary-type")]
        public string PrimaryType { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("artist-credit")]
        public ArtistCredit ArtistCredit { get; set; }

        [XmlElement("first-release-date")]
        public string FirstReleaseDate { get; set; }

        [XmlArray("release-list")]
        [XmlArrayItem("release")]
        public List<Release> Releases { get; set; }

        [XmlIgnore]
        public DateTime? FirstReleaseDateAsDateTime
        {
            get
            {
                return !string.IsNullOrEmpty(FirstReleaseDate) ? Convert.ToDateTime(FirstReleaseDate) : (DateTime?)null;
            }
        }

        [XmlIgnore]
        public int? FirstReleaseYear
        {
            get
            {
                int result;
                if (!string.IsNullOrEmpty(FirstReleaseDate) && FirstReleaseDate.Length >= 4 && int.TryParse(FirstReleaseDate.Substring(0, 4), out result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
