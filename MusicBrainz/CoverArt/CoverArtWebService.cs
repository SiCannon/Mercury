using System;
using System.Web.Script.Serialization;
using MusicBrainz.QueryResultCache;

namespace MusicBrainz.CoverArt
{
    public class CoverArtWebService
    {
        public static CoverArtQueryResult GetByReleaseGroupId(Guid id)
        {
            string result = QueryService.RunQuery("http://coverartarchive.org/release-group/" + id.ToString());
            if (!string.IsNullOrEmpty(result))
            {
                var ser = new JavaScriptSerializer();
                var q = ser.Deserialize<CoverArtQueryResult>(result);
                return q;
            }
            else
            {
                return null;
            }
        }

        public static string GetSmallThumbnailByReleaseGroupId(Guid id)
        {
            var q = GetByReleaseGroupId(id);
            if (q != null)
            {
                return q.SmallThumbnailUrl;
            }
            else
            {
                return null;
            }
        }
    }
}
