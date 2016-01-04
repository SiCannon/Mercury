using System.Collections.Generic;
using Rema.Domain.Entity;
using Rema.Domain.Service.Abstract;

namespace Rema.Domain.Service.Concrete
{
    public class RecordingService : IRecordingService
    {
        string recordingFields = "SOUNDREC_SITE, SOUNDREC_CODE, SONGS_SITE, SONGS_CODE, SOUNDREC_ARTIST, SOUNDREC_SUBMIX, SOUNDREC_ISRC, SOUNDREC_TIME, COMPANY_CODE, LABEL_CODE";

        public List<Recording> ListBySite(string site)
        {
            string query = string.Format("select {0} from SOUNDREC where SOUNDREC_SITE = '{1}'", recordingFields, site);
            return Helpers.Database.GetList(query, x => new Recording(x));
        }

    }
}
