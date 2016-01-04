using System;

namespace Memphis.Database.Entity
{
    public class ArtistTag
    {
        public int? ArtistTagId { get; set; }
        public int? ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public int Count { get; set; }
    }
}
