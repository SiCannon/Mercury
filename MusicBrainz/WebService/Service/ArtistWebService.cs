using System;
using System.Linq;
using MusicBrainz.Helpers;
using MusicBrainz.QueryResultCache;
using MusicBrainz.WebService.Entity;

namespace MusicBrainz.WebService.Service
{
    public class ArtistWebService
    {
        public static ArtistSearchResultWrapper QueryByName(string name)
        {
            string url = Url.Query("artist", "artist", name);
            string xml = QueryService.Fetch(url);
            return Xml.DeserializeMbz<ArtistSearchResultWrapper>(xml);
        }

        public static Artist GetById(Guid id, bool includeReleaseGroups = false)
        {
            string url = Url.Get("artist", id, "tags", includeReleaseGroups ? "release-groups" : "");
            string xml = QueryService.Fetch(url);
            var queryResult = Xml.DeserializeMbz<ArtistQueryResult>(xml);
            return queryResult != null ? queryResult.Artist : null;
        }

        public static Artist FindByName(string name, string hintReleaseGroupTitle = null)
        {
            bool ignoreCase = true;
            StringComparison sc = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            // First try to find a main name match
            var artistsByName = QueryByName(name).Artists.Where(artist => artist.Name.Equals(name, sc));
            if (artistsByName.Count() == 1)
                return artistsByName.ElementAt(0);

            // If multiple found then look in releases
            if (artistsByName.Count() > 1 && !string.IsNullOrEmpty(hintReleaseGroupTitle))
            {
                foreach (var artist in artistsByName)
                {
                    var artistWithReleases = GetById(artist.ArtistId, true);
                    var releaseGroup = artistWithReleases.ReleaseGroups.FirstOrDefault(x => x.Title == hintReleaseGroupTitle);
                    if (releaseGroup != null)
                    {
                        return artist;
                    }
                }
            }

            // If none found then look in aliases
            if (artistsByName.Count() == 0)
            {
                var artistsByAlias = QueryByName(name).Artists.Where(artist => artist.Aliases.Any(alias => alias.Equals(name, sc)));
                if (artistsByAlias.Count() == 1)
                    return artistsByAlias.ElementAt(0);
            }

            // None found
            return null;
        }

    }
}
