using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using MusicBrainz.Helpers;
using MusicBrainz.WebService.Service;
using Top3000Albums.Entity;
using Top3000Albums.Service;
using Cons = System.Console;

namespace Mercury.Console.Generate
{
    class SongsForMrm
    {
        static bool stopped = false;

        /*public static void GoThread()
        {
            stopped = false;
            var t = new Thread(new ThreadStart(Go));
            t.Start();
            while (!t.IsAlive) ;
            Thread.Sleep(1000);
            Cons.ReadKey();
            stopped = true;
            t.Join();
        }*/

        public static void Go()
        {
            List<Album> albums = T3kAlbumService.Read();
            SongExportList songs = new SongExportList();

            foreach (var album in albums)
            {
                if (album.MbzReleaseGroupIdAsGuid.HasValue)
                {
                    if (stopped)
                    {
                        break;
                    }

                    Cons.WriteLine("Album \"{0}\", MbzId = {1}", album.Title, album.MbzReleaseGroupId);

                    var releaseGroup = ReleaseGroupWebService.Query(album.MbzReleaseGroupIdAsGuid.Value);
                    if (releaseGroup.Releases.Count == 0)
                    {
                        Cons.WriteLine("Error: no releases!");
                        continue;
                    }

                    var release = ReleaseWebService.Query(releaseGroup.Releases[0].ReleaseId);
                    if (release.Mediums.Count == 0)
                    {
                        Cons.WriteLine("Error: no mediums");
                        continue;
                    }
                    if (release.Mediums[0].Tracks.Count == 0)
                    {
                        Cons.WriteLine("Error: medium has no tracks");
                        continue;
                    }
                    
                    foreach (var track in release.Mediums[0].Tracks)
                    {
                        if (stopped)
                        {
                            break;
                        }

                        var recording = RecordingWebService.Query(track.Recording.RecordingId);
                        if (recording.Relations.Count == 0)
                        {
                            continue;
                        }

                        Cons.Write("    {0}: \"{1}\"", track.Number, recording.Title);
                        var work = WorkWebService.Query(recording.Relations[0].Target);

                        string composersDisplayText = "";

                        var composers = work.Relations.Where(r => r.Type == "composer");
                        if (composers.Count() == 0)
                        {
                            if (releaseGroup.ArtistCredit.NameCredits.Count > 0)
                            {
                                composersDisplayText = releaseGroup.ArtistCredit.NameCredits[0].Name;
                            }
                        }
                        else
                        {
                            var comps = new StringBuilder();
                            foreach (var c in composers)
                            {
                                var composer = ArtistWebService.GetById(c.Target);
                                string[] nameSplit = composer.Name.Split(' ');
                                comps.Append(nameSplit[nameSplit.Length - 1]);
                                comps.Append("/");
                                
                            }
                            if (comps.Length > 0)
                            {
                                comps.Remove(comps.Length - 1, 1);
                            }
                            composersDisplayText = comps.ToString();
                        }

                        Cons.Write(" ({0}) ", composersDisplayText);

                        songs.Add(new Song
                        {
                            Title = work.Title,
                            Composers = composersDisplayText,
                            Iswc = work.Iswc
                        });

                        Cons.WriteLine("{0}", songs.Count);
                    }
                }

                if (songs.Count >= 1237)
                {
                    break;
                }
            }

            Xml.Serialize(songs, @"c:\temp\songs.xml");
        }
    }

    public class Song
    {
        public string Title { get; set; }
        public string Composers { get; set; }
        public string Iswc { get; set; }
    }

    [XmlRoot("Songs")]
    public class SongExportList : List<Song>
    {

    }
}
