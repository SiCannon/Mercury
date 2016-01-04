using System;
using System.IO;
using System.Net;
using Memphis.BusinessLogic.Interface;
using MusicBrainz.CoverArt;
using MusicBrainz.Helpers;

namespace Mercury.Console.Generate
{
    public class CoverArtPopulator
    {
        public static void Go(IAlbumService albumService)
        {
            int counter = 0;
            int total = albumService.Count("title", null);
            foreach (var album in albumService.GetAll())
            {
                ConsoleHelpers.WriteToConsole("{0}/{1} ", ConsoleColor.Gray, false, ++counter, total);
                ConsoleHelpers.WriteToConsole(album.Title, ConsoleColor.White, false);
                if (album.MusicBrainzReleaseGroupId.HasValue)
                {
                    var qr = CoverArtWebService.GetByReleaseGroupId(album.MusicBrainzReleaseGroupId.Value);
                    if (qr.images.Count > 0)
                    {
                        if (qr.images[0].thumbnails != null)
                        {
                            string url = null;
                            string small = qr.images[0].thumbnails.small;
                            string large = qr.images[0].thumbnails.large;
                            if (!string.IsNullOrEmpty(small))
                            {
                                url = small;
                                ConsoleHelpers.WriteToConsole(" found small... ", ConsoleColor.Green, false, small);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(large))
                                {
                                    url = large;
                                    ConsoleHelpers.WriteToConsole(" found large... ", ConsoleColor.Yellow, false, large);
                                }
                                else
                                {
                                    ConsoleHelpers.WriteToConsole(" thumbnails are both empty", ConsoleColor.Magenta, true);
                                }
                            }
                            if (!string.IsNullOrEmpty(url))
                            {
                                using (var client = new WebClient())
                                {
                                    client.DownloadFile(url, @"..\..\..\Memphis.Website\CoverArt\Album\" + album.AlbumId.ToString() + Path.GetExtension(url));
                                }
                                ConsoleHelpers.WriteToConsole("saved", ConsoleColor.Cyan, true);
                            }
                        }
                        else
                        {
                            ConsoleHelpers.WriteToConsole(" query result contains no thumbnails", ConsoleColor.Magenta, true);
                        }
                    }
                    else
                    {
                        ConsoleHelpers.WriteToConsole(" query result contains no images", ConsoleColor.Magenta, true);
                    }
                }
                else
                {
                    ConsoleHelpers.WriteToConsole(" missing Mbz Release Group ID", ConsoleColor.Magenta, true);
                }

            }
        }
    }
}
