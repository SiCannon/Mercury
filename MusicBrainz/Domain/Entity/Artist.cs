using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MusicBrainz.Domain.Entity
{
    public class Artist
    {
        public int? ArtistId { get; set; }

        public Guid? MbzArtistId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Type { get; set; }

        public int? Score { get; set; }

        public string SearchStatus { get; set; }

        public override string ToString()
        {
            return string.Format("ID={0} MBID={4} Name={1} Type={2} Score={3}", ArtistId, Name, Type, Score, MbzArtistId);
        }
    }
}
