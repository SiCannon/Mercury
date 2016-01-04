using System;
using System.Configuration;
using System.Text;
using System.Web;

namespace MusicBrainz.Helpers
{
    public static class Url
    {
        public static string Root
        {
            get
            {
                const string key = "MusicBrainzUrl";
                string res = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrEmpty(res))
                    throw new Exception("MusicBrainzUrl app setting not found");

                return res + "/ws/2";
            }
        }

        class EntityNames
        {
            public const string Artist = "artist";
            public const string ReleaseGroup = "release-group";
        }

        public static string Query(string entity, string parameter, string query)
        {
            return string.Format("{0}/{1}/?query={2}:{3}", Root, entity, parameter, PrepareQuery(query));
        }

        public static string Query(string entity, string query)
        {
            return string.Format("{0}/{1}/?query={2}", Root, entity, PrepareQuery(query));
        }

        public static string Get(string entity, Guid id, params string[] inc)
        {
            return string.Format("{0}/{1}/{2}{3}", Root, entity, id.ToString(), MakeIncText(inc));
        }

        public static string QueryArtistByName(string name)
        {
            // This includes the artist: parameter even though it's redundant so as to use cache results which were stored before I realized you didn't need it.
            return Query(EntityNames.Artist, "artist", name);
        }

        public static string SearchReleaseGroup(string name)
        {
            return Query(EntityNames.ReleaseGroup, name);
        }

        private static string PrepareQuery(string query)
        {
            //return HttpUtility.UrlEncode("\"" + query.Replace("'", "’") + "\"");
            return HttpUtility.UrlEncode("\"" + query + "\"");
        }

        public static string ReplaceApostrophe(string query)
        {
            return query.Replace("'", "’");
        }

        private static string MakeIncText(params string[] inc)
        {
            if (inc.Length == 0 || (inc.Length == 1 && string.IsNullOrEmpty(inc[0])))
                return "";
            else
            {
                StringBuilder sb = new StringBuilder("?inc=");
                foreach (string s in inc)
                {
                    sb.Append(s);
                    sb.Append("+");
                }
                sb.Remove(sb.Length - 1, 1);
                return sb.ToString();
            }
        }

    }
}
