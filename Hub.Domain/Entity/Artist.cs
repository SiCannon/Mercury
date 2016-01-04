using System;

namespace Hub.Domain.Entity
{
    public class Artist
    {
        public int? ArtistId { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", ArtistId, Name);
        }
    }
}
