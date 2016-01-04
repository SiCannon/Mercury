using System;
using System.Collections.Generic;
using System.Linq;
using MusicBrainz.Helpers;
using MusicBrainz.QueryResultCache;
using MusicBrainz.WebService.Entity;

namespace MusicBrainz.WebService.Service
{
    public class ReleaseGroupWebService
    {
        public static List<ReleaseGroup> Search(string query)
        {
            string releaseGroupXml = QueryService.Fetch(Url.SearchReleaseGroup(query));

            var metadata = Xml.DeserializeMbz<ReleaseGroupSearchResults>(releaseGroupXml);

            return metadata.ReleaseGroups;
        }

        private static ReleaseGroup GetByArtistAndNameSingle(string artistName, string albumName, bool return100score = false, bool usePrimary = false, bool ignoreCase = true)
        {
            var sc = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            var rgs = Search(albumName);
            var res = rgs.Where(r =>
                ((usePrimary && r.PrimaryType == "Album") || (!usePrimary && r.Type == "Album")) &&
                (string.Equals(r.Title, albumName, sc) || (return100score && r.Score == 100)) &&
                r.ArtistCredit.NameCredits.Any(n => string.Equals(n.Name, artistName, sc)));
            if (res.Count() == 1)
                return res.ElementAt(0);
            else
                return null;
        }

        public static ReleaseGroup GetByArtistAndName(string artistName, string albumName)
        {
            ReleaseGroup result;

            result = GetByArtistAndNameSingle(artistName, albumName);
            if (result != null)
                return result;

            result = GetByArtistAndNameSingle(artistName, albumName.Replace("'", "’"));
            if (result != null)
                return result;

            result = GetByArtistAndNameSingle(artistName, albumName, true);
            if (result != null)
                return result;

            result = GetByArtistAndNameSingle(artistName, albumName.Replace("'", "’"), true);
            if (result != null)
                return result;

            result = GetByArtistAndNameSingle(artistName.Replace(" and ", " & "), albumName);
            if (result != null)
                return result;

            result = GetByArtistAndNameSingle(artistName.Replace(" & ", " and "), albumName);
            if (result != null)
                return result;

            result = GetByArtistAndNameSingle(artistName, albumName, false, true);
            if (result != null)
                return result;

            result = GetByArtistAndNameSingle(artistName, albumName, true, true);
            if (result != null)
                return result;

            return null;
        }

        public static ReleaseGroup Query(Guid id)
        {
            string xml = QueryService.Fetch(Url.Get("release-group", id, "releases", "artists"));
            return Xml.DeserializeMbz<ReleaseGroupQueryResult>(xml).ReleaseGroup;
        }
    }
}
