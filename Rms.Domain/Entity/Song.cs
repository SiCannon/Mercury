using System;

namespace Rms.Domain.Entity
{
    using RemaSong = Rema.Domain.Entity.Song; 

    public class Song
    {
        public int? SongId { get; set; }
        public string OriginalId { get; set; }
        public string Title { get; set; }
        public string Iswc { get; set; }

        public Song()
        {

        }

        public Song(RemaSong remaSong)
        {
            OriginalId = string.Format("{0}.{1}", remaSong.Site, remaSong.Code);
            Title = remaSong.Title;
            Iswc = remaSong.Iswc;
        }
    }
}
