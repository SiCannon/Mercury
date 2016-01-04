using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Hub.Domain.Entity;
using Hub.Domain.Infrastructure;
using Top3000Albums.Entity;
using Top3000Albums.Service;

namespace Mercury.Console.Test
{
    class HubContextTests
    {
        private static List<Album> Albums
        {
            get
            {
                return T3kAlbumService.Read(@".\Data\Top3000Albums.xml");
            }
        }

        private static List<Artist> MakeArtistList()
        {
            return Albums
                //.Take(100)
                .Select(a => a.Artist)
                .Distinct()
                .Select((a, n) => new Artist { ArtistId = n + 1, Name = a })
                .ToList();
        }

        public static void ListArtistsToConsole()
        {
            var c = new HubContext();

            foreach (var a2 in c.Artists)
                System.Console.WriteLine(a2.Name);
        }

        public static void TestMakeArtistsFromTop3000()
        {
            var artists = MakeArtistList();

            foreach (var a in artists)
            {
                System.Console.WriteLine("{0} {1}", a.ArtistId, a.Name);
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Artist count: {0}", artists.Count);

            XmlSerializer writer = new XmlSerializer(artists.GetType());
            StreamWriter file = new StreamWriter(@"c:\temp\artist.xml");
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            writer.Serialize(file, artists, ns);
            file.Close();
        }

        public static void TestEmptyArtist()
        {
            var emptyCount = Albums.Where(a => string.IsNullOrWhiteSpace(a.Artist)).Count();
            System.Console.WriteLine(emptyCount.ToString());
        }

        public static void TestUnicode()
        {
            var artists = MakeArtistList();
            var huskerDu = artists.Single(a => a.ArtistId == 143);
            System.Console.WriteLine("{0} {1}", huskerDu.ArtistId, huskerDu.Name);

            char c = huskerDu.Name[1];
            System.Console.WriteLine((byte)c);
        }

        public static void TestIdentityInsert()
        {
            var ctx = new HubContext();
            
            System.Console.WriteLine("Before insert there are {0} artists:", ctx.Artists.Count().ToString());
            foreach (var artist in ctx.Artists)
                System.Console.WriteLine("  " + artist.ToString());

            System.Console.WriteLine();
            
            var a1 = new Artist { ArtistId = 100, Name = "Simon" };
            System.Console.WriteLine("adding artist " + a1.ToString());
            ctx.Artists.Add(a1);
            ctx.SaveChanges();

            System.Console.WriteLine();
            var ctx2 = new HubContext();
            System.Console.WriteLine("After insert there are {0} artists:", ctx2.Artists.Count().ToString());
            foreach (var artist in ctx2.Artists)
                System.Console.WriteLine("  " + artist.ToString());

        }
    }
}
