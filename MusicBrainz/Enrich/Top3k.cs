using System;
using System.Collections.Generic;
using System.Linq;
using MusicBrainz.WebService.Entity;
using MusicBrainz.WebService.Service;
using Top3000Albums.Entity;
using Top3000Albums.Service;

namespace MusicBrainz.Enrich
{
    public class Top3k
    {
        public static bool Stopped { get; set; }

        public static void PopulateMbzArtistIds()
        {
            var albums = T3kAlbumService.Read();
            for (int i = 0; i < albums.Count; i++)
            {
                if (!albums[i].MbzArtistIdAsGuid.HasValue)
                {
                    var artist = ArtistWebService.FindByName(albums[i].Artist);
                    if (artist != null)
                    {
                        albums[i].MbzArtistIdAsGuid = artist.ArtistId;
                    }
                }
                if (i % 100 == 0)
                    Console.WriteLine("{0} albums processed", i);
            }
            Console.WriteLine("{0} albums processed", albums.Count);
            T3kAlbumService.Write(albums);
        }

        public static void PopulateMbzReleaseGroupIds()
        {
            Stopped = false;
            var albums = T3kAlbumService.Read();
            for (int i = 0; i < albums.Count && !Stopped; i++)
            {
                WriteToConsole("{0}/{1}", ConsoleColor.Gray, false, i + 1, albums.Count);
                WriteToConsole(" {0}", ConsoleColor.Yellow, false, albums[i].Artist);
                WriteToConsole(" {0}", ConsoleColor.White, false, albums[i].Title);

                // Get the release group ID if it's missing
                if (!albums[i].MbzReleaseGroupIdAsGuid.HasValue)
                {
                    var rg = ReleaseGroupWebService.GetByArtistAndName(albums[i].Artist, albums[i].Title);
                    if (rg != null)
                    {
                        albums[i].MbzReleaseGroupIdAsGuid = rg.ReleaseGroupId;
                    }
                }

                // Get the artist if it's missing
                if (albums[i].MbzReleaseGroupIdAsGuid.HasValue && !albums[i].MbzArtistIdAsGuid.HasValue)
                {
                    var rg = ReleaseGroupWebService.GetByArtistAndName(albums[i].Artist, albums[i].Title);
                    if (rg != null)
                    {
                        var artists = rg.ArtistCredit.NameCredits.Where(n => string.Equals(n.Name, albums[i].Artist, StringComparison.OrdinalIgnoreCase));
                        if (artists.Count() == 1)
                        {
                            albums[i].MbzArtistIdAsGuid = artists.ElementAt(0).ArtistId;
                        }
                    }
                }

                if (albums[i].MbzReleaseGroupIdAsGuid.HasValue)
                    WriteToConsole(" ok", ConsoleColor.Green, true);
                else
                {
                    // Do an artist search


                    WriteToConsole(" not found", ConsoleColor.Red, true);
                }
            }

            T3kAlbumService.Write(albums);
        }

        private static void FindOneReleaseGroup()
        {

        }

        private static void WriteToConsole(string format, ConsoleColor color, bool newLine, params object[] arg)
        {
            Console.ForegroundColor = color;
            Console.Write(format, arg);
            if (newLine)
                Console.WriteLine();
        }

        public static void QueryAllReleaseGroups()
        {
            Stopped = false;
            int guidLen = Guid.Empty.ToString().Length;
            var albums = T3kAlbumService.Read();
            for (int i = 0; i < albums.Count && !Stopped; i++)
            {
                WriteToConsole("{0}/{1}", ConsoleColor.Gray, false, i + 1, albums.Count);
                if (albums[i].MbzReleaseGroupIdAsGuid.HasValue)
                {
                    WriteToConsole(" {0}", ConsoleColor.Magenta, false, albums[i].MbzReleaseGroupIdAsGuid);
                    WriteToConsole(" {0} - {1}", ConsoleColor.Yellow, false, albums[i].Artist, albums[i].Title);
                    try
                    {
                        var rg = ReleaseGroupWebService.Query(albums[i].MbzReleaseGroupIdAsGuid.Value);
                        WriteToConsole(" ok", ConsoleColor.Green, true);
                    }
                    catch (Exception ex)
                    {
                        WriteToConsole(" exception : {0}", ConsoleColor.Red, true, ex.Message);
                    }
                }
                else
                {
                    WriteToConsole(" {0," + guidLen + "}", ConsoleColor.DarkMagenta, false, "no release group id");
                    WriteToConsole(" {0} - {1}", ConsoleColor.DarkYellow, false, albums[i].Artist, albums[i].Title);
                    WriteToConsole(" skipped", ConsoleColor.DarkGreen, true);
                }
            }
        }

        public static void PopulateOneAlbum(int albumId)
        {
            var albums = T3kAlbumService.Read();
            var album = albums.SingleOrDefault(a => a.AlbumIdAsInteger == albumId);
            if (album == null)
            {
                Console.WriteLine("Album {0} not found", albumId);
                return;
            }
            else
            {
                Console.WriteLine("Getting information for {0} - {1}", album.Artist, album.Title);
            }

            if (album.MbzArtistIdAsGuid.HasValue)
            {
                Console.WriteLine("MbzArtistId already found: {0}", album.MbzArtistIdAsGuid);
            }
            else
            {
                Console.Write("Artist ID: ");
                var artist = ArtistWebService.FindByName(album.Artist);
                if (artist != null)
                {
                    album.MbzArtistIdAsGuid = artist.ArtistId;
                    T3kAlbumService.Write(albums);
                    WriteToConsole("{0}", ConsoleColor.Magenta, true, album.MbzArtistIdAsGuid);
                }
                else
                {
                    WriteToConsole("not found", ConsoleColor.Red, true);
                }
            }

            if (album.MbzReleaseGroupIdAsGuid.HasValue)
            {
                Console.WriteLine("MbzReleaseGroupId already found: {0}", album.MbzReleaseGroupIdAsGuid);
            }
            else
            {
                Console.ResetColor();
                Console.Write("Release Group ID: ");
                var rg = ReleaseGroupWebService.GetByArtistAndName(album.Artist, album.Title);
                if (rg != null)
                {
                    album.MbzReleaseGroupIdAsGuid = rg.ReleaseGroupId;
                    T3kAlbumService.Write(albums);
                    WriteToConsole("{0}", ConsoleColor.Magenta, true, album.MbzReleaseGroupIdAsGuid);
                }
                else
                {
                    WriteToConsole("not found", ConsoleColor.Red, true);
                }
            }
        }
    }
}
