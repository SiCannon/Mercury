using System.Collections.Generic;
using System.Data;
using Oracle.DataAccess.Client;
using Rema.Domain.Entity;
using Rema.Domain.Service.Abstract;

namespace Rema.Domain.Service.Concrete
{
    public class SongService : ISongService
    {
        public Song GetBySiteAndCode(string site, int code)
        {
            string query = string.Format("select SONGS_SITE, SONGS_CODE, SONGS_TITLE, SONGS_ISWC, SONGS_COMPOSER from SONGS where SONGS_SITE = '{0}' and SONGS_CODE = {1}", site, code);

            using (OracleConnection con = new OracleConnection(Helpers.Config.GetAppSetting("RemaConnectionString")))
            using (OracleCommand cmd = new OracleCommand(query, con))
            {
                cmd.CommandType = CommandType.Text;
                con.Open();
                var rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    return new Song(rdr);
                }
                else
                {
                    return null;
                }
            }
        }

        public List<Song> ListBySite(string site, bool mustHaveIswc, int? maxResults = null)
        {
            string iswcFilter = mustHaveIswc ? "and Trim(SONGS_ISWC) is not null" : "";
            string rowNumFilter = maxResults.HasValue ? string.Format("and rownum <= {0}", maxResults) : "";
            string query = string.Format("select SONGS_SITE, SONGS_CODE, SONGS_TITLE, SONGS_ISWC, SONGS_COMPOSER from SONGS where SONGS_SITE = '{0}' {1} {2}", site, iswcFilter, rowNumFilter);

            using (OracleConnection con = new OracleConnection(Helpers.Config.GetAppSetting("RemaConnectionString")))
            using (OracleCommand cmd = new OracleCommand(query, con))
            {
                cmd.CommandType = CommandType.Text;
                con.Open();
                var rdr = cmd.ExecuteReader();
                var result = new List<Song>();
                while (rdr.Read())
                {
                    result.Add(new Song(rdr));
                }
                return result;
            }
  
        }

        #region private

        //OracleConnection con = new OracleConnection(Helpers.Config.GetAppSetting("RemaConnectionString"));

        #endregion
    }
}
