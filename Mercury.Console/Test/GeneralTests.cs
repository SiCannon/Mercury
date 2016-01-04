using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mercury.Console.Test
{
    class GeneralTests
    {
        public static void TestReadAnsiFile()
        {
            using (StreamReader file = new StreamReader(@"c:\temp\husker.txt"))
            {
                char[] buffer = new char[1000];
                int size = file.Read(buffer, 0, 1000);
                //string s = file.ReadToEnd();
                //System.Console.WriteLine(s);
                for (int i = 0; i < size; i++)
                    System.Console.WriteLine(buffer[i] + " " + (byte)buffer[i]);
            }
        }

        public static void TestReadLargeFile()
        {
            using (StreamReader file = new StreamReader(@"C:\Users\Simon\Downloads\mbdump.tar\mbdump\mbdump\artist_name"))
            {
                int counter = 0;
                while (!file.EndOfStream)
                {
                    string s = file.ReadLine();

                    //int pTab = s.Split(char.

                    counter++;
                    if (counter % 10000 == 0)
                    {
                        //System.Console.WriteLine(s);
                        System.Console.WriteLine("{0} rows inserted {1}", counter, s);
                    }
                }
            }
        }

        public static void TestUrlPathEncode()
        {
            System.Console.WriteLine(HttpUtility.UrlPathEncode(string.Format("http://www.musicbrainz.org/ws/2/artist/?query=artist:{0}", "Led Zeppelin")));
        }

        public static void TestConsolePaging()
        {
            var albums = Top3000Albums.Service.T3kAlbumService.Read();

            int pageNum = 0;
            int pageSize = 40;

            bool onlyMissingArtists = false;

            bool quit = false;
            while (!quit)
            {

                System.Console.Clear();
                int startIndex = pageNum * pageSize;
                var displayAlbums = onlyMissingArtists ? albums.Where(a => !a.MbzArtistIdAsGuid.HasValue) : albums;
                var albumPage = displayAlbums.Skip(startIndex).Take(pageSize);
                foreach (var a in albumPage)
                    System.Console.WriteLine("{0,5} {1}  {2}  {3}  {4}", a.AlbumIdAsInteger,
                        a.Artist.PadRight(30).Substring(0, 30),
                        a.Title.PadRight(30).Substring(0, 30),
                        a.MbzArtistIdAsGuid.ToString().PadRight(8).Substring(0, 8),
                        a.MbzReleaseGroupIdAsGuid.ToString().PadRight(8).Substring(0, 8));
                System.Console.WriteLine();
                System.Console.WriteLine("page {0}", pageNum + 1);

                switch (System.Console.ReadKey().Key)
                {
                    case ConsoleKey.Q:
                        quit = true;
                        break;
                    case ConsoleKey.A:
                        onlyMissingArtists = !onlyMissingArtists;
                        break;
                    case ConsoleKey.DownArrow:
                        pageNum++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (pageNum > 0)
                            pageNum--;
                        break;
                }

            }

            Environment.Exit(0);
        }
    }
}
