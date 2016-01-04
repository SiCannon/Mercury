using System;
using System.Data;

namespace Rema.Domain.Entity
{
    public class Song
    {
        public string Site { get; set; }
        public int Code { get; set; }
        public string Title { get; set; }
        public string Iswc { get; set; }
        public string Composers { get; set; }

        public Song()
        {

        }

        public Song(IDataRecord row)
        {
            Site = Helpers.Database.GetString(row, "SONGS_SITE");
            Code = Helpers.Database.GetInt32(row, "SONGS_CODE") ?? -1;
            Title = Helpers.Database.GetString(row, "SONGS_TITLE");
            Iswc = Helpers.Database.GetString(row, "SONGS_ISWC");
            Composers = Helpers.Database.GetString(row, "SONGS_COMPOSER");
        }
    }
}
