using System;
using System.Linq;
using MusicBrainz.Domain.Entity;
using MusicBrainz.Domain.Service;

namespace MusicBrainz.Populate
{
    using WebService = MusicBrainz.WebService.Service.ArtistWebService;

    public class PopulateArtists
    {
        public static void CreateSkeletonRecordsFromTop3k()
        {
            Console.WriteLine("creating skeleton artist records...");
            (new ArtistService()).SaveMultipleNewByName(Top3000Albums.Service.T3kAlbumService.ListDistinctArtistNames());
        }

        public static void GetMbzIDs(bool retryFailed = false)
        {
            Console.WriteLine("getting MusicBrainz Artist IDs...");
            
            var service = new ArtistService();

            foreach (var artist in service.Artists.ToList())
            {
                Console.Write(artist.Name);
                if (artist.MbzArtistId.HasValue)
                {
                    Console.WriteLine(" already got: {0}", artist.MbzArtistId);
                }
                else if (!string.IsNullOrEmpty(artist.SearchStatus) && !retryFailed)
                {
                    Console.WriteLine(" already failed: {0}", artist.SearchStatus);
                }
                else
                {
                    var artists = WebService.QueryByName(artist.Name).Artists.Where(a => a.Name == artist.Name || a.Aliases.Contains(artist.Name));
                    if (artists.Count() == 1)
                    {
                        artist.MbzArtistId = artists.Single().ArtistId;
                        Console.WriteLine(" ok: {0}", artist.MbzArtistId);
                    }
                    else
                    {
                        artist.SearchStatus = string.Format("{0} search results", artists.Count());
                        Console.WriteLine(" failed: {0}", artist.SearchStatus);
                    }
                    service.Save(artist);
                }
            }
        }
    }
}
