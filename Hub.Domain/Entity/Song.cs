using System;

namespace Hub.Domain.Entity
{
    public class Song
    {
        public int? SongId { get; set; }
        public string Title { get; set; }
        public string Iswc { get; set; }
        public string Composers { get; set; }
    }
}
