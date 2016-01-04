using System;
using System.Linq;
using Top3000Albums.Service;

namespace Mercury.Console.Test
{
    class Top3000AlbumsTests
    {
        public static void TestRead()
        {
            var albums = T3kAlbumService.Read(@".\Data\Top3000Albums.xml");
            int i = 0;
            foreach (var a in albums)
                System.Console.WriteLine("{0} {1} {2}", i++, a.Title, a.Artist);
        }

        public static void TestOddCharacters()
        {
            var albums = T3kAlbumService.Read(@".\Data\Top3000Albums.xml");
            WriteStringAndChars(albums.Single(a => a.PlaceAsInt == 204).Artist);
            WriteStringAndChars(albums.Single(a => a.PlaceAsInt == 139).Artist);
        }

        private static void WriteStringAndChars(string s)
        {
            System.Console.WriteLine(s);
            foreach (char c in s)
            {
                System.Console.WriteLine("    {0} {1}", c, Convert.ToInt32(c));
            }
        }

        public static void TestWrite()
        {
            var albums = T3kAlbumService.Read();
            //albums.ElementAt(0).Artist = "HELLO";
            T3kAlbumService.Write(albums);
        }
    }
}
