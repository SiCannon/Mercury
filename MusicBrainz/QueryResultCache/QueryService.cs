using System;
using System.Linq;
using System.Net;

namespace MusicBrainz.QueryResultCache
{
    class QueryService
    {
        public static string Fetch(string queryString)
        {
            bool isRemote = queryString.StartsWith(@"http://www.musicbrainz.org/");

            if (isRemote)
            {
                var ctx = new MbzqContext();
                var qry = ctx.Queries.FirstOrDefault(q => q.QueryString == queryString);

                if (qry == null)
                {
                    LimitRate();
                    string result = RunQuery(queryString);
                    qry = new Query { QueryString = queryString, Result = result };
                    ctx.Queries.Add(qry);
                    ctx.SaveChanges();
                }

                return qry.Result;
            }
            else
            {
                return RunQuery(queryString);
            }
            
        }

        private static void LimitRate()
        {
            TimeSpan timeSinceLastQuery = DateTime.Now - lastQueryTime;
            if (timeSinceLastQuery < minimumTimeBetweenQueries)
            {
                TimeSpan timeUntilNextQuery = minimumTimeBetweenQueries - timeSinceLastQuery;
                System.Threading.Thread.Sleep(Convert.ToInt32(timeUntilNextQuery.TotalMilliseconds));
            }
        }

        public static string RunQuery(string queryString)
        {
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            try
            {
                string result = wc.DownloadString(queryString);
                lastQueryTime = DateTime.Now;
                return result;
            }
            catch (WebException ex)
            {
                if (ex.Response != null && CanIgnoreErrorCode(((System.Net.HttpWebResponse)ex.Response).StatusCode))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        private static bool CanIgnoreErrorCode(HttpStatusCode code)
        {
            return code == HttpStatusCode.NotFound
                || code == HttpStatusCode.Forbidden;
        }

        private static DateTime lastQueryTime = DateTime.MinValue;
        private static TimeSpan minimumTimeBetweenQueries = new TimeSpan(0, 0, 1);
    }
}
