using System;
using MusicBrainz.Helpers;
using MusicBrainz.QueryResultCache;
using MusicBrainz.WebService.Entity;

namespace MusicBrainz.WebService.Service
{
    public class ReleaseWebService
    {
        public static Release Query(Guid id)
        {
            string xml = QueryService.Fetch(Url.Get("release", id, "discids", "labels", "recordings"));
            return Xml.DeserializeMbz<ReleaseQueryResult>(xml).Release;
        }
    }
}
