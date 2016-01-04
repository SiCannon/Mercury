using System;
using System.Data;

namespace Rema.Domain.Entity
{
    public class Track
    {
        public string ProductSite { get; set; }
        public string ProductCode { get; set; }
        public int Side { get; set; }
        public int Number { get; set; }
        public string Sub { get; set; }
        public string RecordingSite { get; set; }
        public int RecordingCode { get; set; }
        public int? Duration { get; set; }

        public Track()
        {

        }

        public Track(IDataRecord row)
        {
            ProductSite = Helpers.Database.GetString(row, "PRODUCT_SITE");
            ProductCode = Helpers.Database.GetString(row, "PRODUCT_CODE");
            Side = Helpers.Database.GetInt32(row, "PRODTRACK_SIDE") ?? -1;
            Number = Helpers.Database.GetInt32(row, "PRODTRACK_TRACK") ?? -1;
            Sub = Helpers.Database.GetString(row, "PRODTRACK_SUB");
            RecordingSite = Helpers.Database.GetString(row, "SOUNDREC_SITE");
            RecordingCode = Helpers.Database.GetInt32(row, "SOUNDREC_CODE") ?? -1;
            Duration = Helpers.Rema.TimeToDuration(Helpers.Database.GetString(row, "PRODTRACK_TRACKTIME"));
        }
    }
}
