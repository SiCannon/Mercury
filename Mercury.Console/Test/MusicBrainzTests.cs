using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using MusicBrainz.Domain.Service;
using MusicBrainz.WebService.Entity;
using MusicBrainz.WebService.Service;
using Top3000Albums.Service;
using Cons = System.Console;

namespace Mercury.Console.Test
{
    static class MusicBrainzTests
    {
        static ArtistService s1 = new ArtistService();
        static ArtistService s2 = new ArtistService();

        public static void TestDatabaseArtist()
        {
            s1.Save(new MusicBrainz.Domain.Entity.Artist { Name = "Pink Floyd" });
        }

        public static void TestDatabaseListArtists()
        {
            foreach (var a in s1.Artists)
            {
                System.Console.WriteLine(a.ToString());
            }
        }

        public static void TestGetArtistByName()
        {
            var artist = ArtistWebService.FindByName("Led Zeppelin");
            System.Console.WriteLine(artist);
        }

        public static void TestXmlSerialization()
        {
            Serialize(@"c:\temp\metadata.xml",
                new ArtistSearchResultWrapper
                {
                    Artists = new List<Artist>
                    {
                        new Artist { Name = "Led Zeppelin", Type = MusicBrainz.Domain.Entity.ArtistType.Group, Score = 100 },
                        new Artist { Name = "Traffic", Type = MusicBrainz.Domain.Entity.ArtistType.Group },
                        new Artist { Name = "Jimi Hendrix", Type = MusicBrainz.Domain.Entity.ArtistType.Person },
                        new Artist { Name = "Pink Floyd" }
                    }
                }
            );
        }

        private static void Serialize(string filename, object o)
        {
            XmlSerializer writer = new XmlSerializer(o.GetType(), "http://musicbrainz.org/ns/mmd-2.0#");
            StreamWriter file = new StreamWriter(filename);
            var ns = new XmlSerializerNamespaces();
            ns.Add("ext", "http://musicbrainz.org/ns/ext#-2.0");
            writer.Serialize(file, o, ns);
            file.Close();
        }

        public static void TestXmlDeserialization()
        {
            string filename = @"c:\temp\LedZepSearchResult.xml";
            ArtistSearchResultWrapper metadata;

            XmlSerializer reader = new XmlSerializer(typeof(ArtistSearchResultWrapper), "http://musicbrainz.org/ns/mmd-2.0#");
            using (StreamReader file = new StreamReader(filename))
            {
                metadata = (ArtistSearchResultWrapper)reader.Deserialize(file);
            }

            foreach (var a in metadata.Artists)
                System.Console.WriteLine(a.ToString());
        }

        public static void TestArtistSearch()
        {
            var metadata = ArtistWebService.QueryByName("Led Zeppelin");
            foreach (var a in metadata.Artists)
            {
                System.Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write(a.Name);
                System.Console.ForegroundColor = ConsoleColor.Gray;
                System.Console.WriteLine(" {0} {1} {2}", a.Type, a.Score, a.ArtistId);
                System.Console.ForegroundColor = ConsoleColor.White;
            }
        }


        internal static void TestSpecificArtistSearches()
        {
            var artists = ArtistWebService.QueryByName("Snoop Doggy Dogg").Artists.ToList();
            System.Console.WriteLine("{0}", artists.Count);
        }

        public static void GetArtistIds()
        {
            var albums = T3kAlbumService.Read(@".\Data\Top3000Albums.xml");

            foreach (var album in albums.Take(10))
            {
                var artist = ArtistWebService.FindByName(album.Artist);
                if (artist != null)
                {
                    System.Console.WriteLine("Artist {0}: Name = {1}, MBID = {2}, Score = {3}", album.Artist, artist.Name, artist.ArtistId, artist.Score);
                }
                else
                {
                    System.Console.WriteLine("Artist {0} not found in Mbz", album.Artist);
                }
                System.Threading.Thread.Sleep(1100);
            }
        }

        public static void TestSerializeReleaseGroup()
        {
            Serialize(@"c:\temp\rg.xml",
                new ReleaseGroupSearchResults
                {
                    ReleaseGroups = new List<ReleaseGroup>
                    {
                        new ReleaseGroup
                        {
                            ReleaseGroupId = Guid.NewGuid(),
                            Type = "Album",
                            Score = 100,
                            PrimaryType = "Album",
                            Title = "Nevermind",
                            ArtistCredit = new ArtistCredit
                            {
                                NameCredits = new List<Artist>
                                {
                                    new Artist {
                                        ArtistId = Guid.NewGuid(),
                                        Name = "Nirvana"
                                    }
                                }
                            }
                        }
                    }
                });
        }

        public static void TestReleaseGroupWebService()
        {
            foreach (var rg in ReleaseGroupWebService.Search("Nevermind"))
            {
                System.Console.WriteLine("{0} {1} {2}", rg.ReleaseGroupId, rg.PrimaryType, rg.Title);
            }
        }

        public static void TestApostopheSearch()
        {
            var rg = ReleaseGroupWebService.GetByArtistAndName("The Sex Pistols", "Never Mind the Bollocks Here's the Sex Pistols");
            if (rg != null)
                System.Console.WriteLine(rg.ReleaseGroupId);
            else
                System.Console.WriteLine("not found");
        }

        public static void TestReleaseGroupQuery()
        {
            var rg = ReleaseGroupWebService.Query(new Guid("fdd96703-7b21-365e-bdea-38029fbeb84e"));
            System.Console.WriteLine(rg.Title);
            System.Console.WriteLine();
            foreach (var r1 in rg.Releases)
            {
                var r = ReleaseWebService.Query(r1.ReleaseId);

                System.Console.WriteLine("{0} {1} {2} track count = {3}", r.Title, r.Country, r.Date, r.Mediums.Count > 0 ? r.Mediums[0].Tracks.Count :0);
                
            }
        }

        public static void TestDeserialzeUnicode()
        {
            var rg = ReleaseGroupWebService.Query(new Guid("9f7a4c28-8fa2-3113-929c-c47a9f7982c3"));
            System.Console.WriteLine(rg.Title);
            var r = ReleaseWebService.Query(rg.Releases.ElementAt(0).ReleaseId);
            System.Console.WriteLine(r.Title);
        }

        public static void TestRecordingWebService()
        {
            var r = RecordingWebService.Query(new Guid("6004dcdd-e026-444e-8406-844a33d3628a"));
            Cons.WriteLine(r.Title);
            foreach (var isrc in r.Isrcs)
            {
                Cons.WriteLine("ISRC: {0}", isrc.Id);
            }
            foreach (var rel in r.Relations)
            {
                Cons.WriteLine("{0} relation: target = {1}", rel.Type, rel.Target);
            }
        }

        public static void TestWorkWebService()
        {
            var w = WorkWebService.Query(new Guid("8d9c97e9-dde0-3f8d-bfd4-4af58ad2c910"));
            Cons.WriteLine(w.Title);
            Cons.WriteLine(w.Iswc);
            foreach (var rel in w.Relations)
            {
                Cons.WriteLine("{0} relation: target = {1}", rel.Type, rel.Target);
            }
        }

        public static void TestArtistWebService()
        {
            var a = ArtistWebService.GetById(new Guid("634fe78e-fc6b-4b2a-ba83-c8c66e13a8aa")); // brian wilson
            Cons.WriteLine(a.Name);
        }

        public static void TestArtistWebServiceWithReleaseGroups()
        {
            var a = ArtistWebService.GetById(new Guid("678d88b2-87b0-403b-b63d-5da7465aecc3"), true); // led zeppelin
            Cons.WriteLine("Name: " + a.Name);
            Cons.WriteLine("Release Group Count: " + a.ReleaseGroups.Count);
            Cons.WriteLine("Release Group Titles:");
            foreach (var g in a.ReleaseGroups)
            {
                Cons.WriteLine("    " + g.Title);
            }
        }

    }
}
