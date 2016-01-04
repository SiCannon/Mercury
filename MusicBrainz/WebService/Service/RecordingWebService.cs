using System;
using MusicBrainz.Helpers;
using MusicBrainz.QueryResultCache;
using MusicBrainz.WebService.Entity;

namespace MusicBrainz.WebService.Service
{
    public class RecordingWebService
    {
        public static Recording Query(Guid id)
        {
            string xml = QueryService.Fetch(Url.Get("recording", id, "isrcs", "work-rels"));
            return Xml.DeserializeMbz<RecordingQueryResult>(xml).Recording;
        }
    }
}
