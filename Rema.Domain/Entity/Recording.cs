using System.Data;

namespace Rema.Domain.Entity
{
    public class Recording
    {
        public string Site { get; set; }
        public int Code { get; set; }
        public string SongSite { get; set; }
        public int SongCode { get; set; }
        public string Artist { get; set; }
        public string Submix { get; set; }
        public string Isrc { get; set; }
        public int? Duration { get; set; }
        public string CompanyCode { get; set; }
        public string LabelCode { get; set; }

        public Recording()
        {

        }

        public Recording(IDataRecord row)
        {
            Site = Helpers.Database.GetString(row, "SOUNDREC_SITE");
            Code = Helpers.Database.GetInt32(row, "SOUNDREC_CODE") ?? -1;
            SongSite = Helpers.Database.GetString(row, "SONGS_SITE");
            SongCode = Helpers.Database.GetInt32(row, "SONGS_CODE") ?? -1;
            Artist = Helpers.Database.GetString(row, "SOUNDREC_ARTIST");
            Submix = Helpers.Database.GetString(row, "SOUNDREC_SUBMIX");
            Isrc = Helpers.Database.GetString(row, "SOUNDREC_ISRC");
            Duration = Helpers.Rema.TimeToDuration(Helpers.Database.GetString(row, "SOUNDREC_TIME"));
            CompanyCode = Helpers.Database.GetString(row, "COMPANY_CODE");
            LabelCode = Helpers.Database.GetString(row, "LABEL_CODE");
        }

    }
}
