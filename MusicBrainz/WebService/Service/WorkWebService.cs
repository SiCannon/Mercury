using System;
using MusicBrainz.Helpers;
using MusicBrainz.QueryResultCache;
using MusicBrainz.WebService.Entity;

namespace MusicBrainz.WebService.Service
{
    public class WorkWebService
    {
        public static Work Query(Guid id)
        {
            string xml = QueryService.Fetch(Url.Get("work", id, "artist-rels"));
            return Xml.DeserializeMbz<WorkQueryResult>(xml).Work;
        }
    }
}
