using System;
using System.Collections.Generic;
using System.Linq;
using Top3000Albums.Entity;
using Top3000Albums.Service;

namespace Mercury.Console.Gui
{
    class Main
    {
        static List<Album> albums = T3kAlbumService.Read();
        static List<Album> albumPage;
        static int pageNum = 0;
        static int pageSize = 40;

        static bool onlyMissingArtists = false;
        static bool onlyMissingAlbums = false;

        static bool quit = false;

        public static void Loop()
        {
            System.Console.SetWindowSize(160, 46);
            System.Console.SetBufferSize(160, 46);
            

            while (!quit)
            {
                CalcAlbumPage();
                RenderAll();

                if (IsPageEmpty && !IsFirstPage)
                    pageNum--;
                else
                    ReadInput();
            }

            Environment.Exit(0);
        }

        private static void CalcAlbumPage()
        {
            int startIndex = pageNum * pageSize;
            var alb1 = onlyMissingArtists ? albums.Where(a => !a.MbzArtistIdAsGuid.HasValue) : albums;
            var alb2 = onlyMissingAlbums ? albums.Where(a => !a.MbzReleaseGroupIdAsGuid.HasValue) : alb1;
            albumPage = alb2.Skip(startIndex).Take(pageSize).ToList();
        }

        private static bool IsPageEmpty
        {
            get
            {
                return albumPage.Count() == 0;
            }
        }

        private static bool IsFirstPage
        {
            get
            {
                return pageNum == 0;
            }
        }

        private static void RenderAll()
        {
            System.Console.Clear();
            foreach (var a in albumPage)
            {
                WriteToConsole("{0,5}", ConsoleColor.Gray, false, a.AlbumIdAsInteger);
                WriteToConsole(" {0}", ConsoleColor.Yellow, false, a.Artist.Pad(30));
                WriteToConsole(" {0}", ConsoleColor.White, false, a.Title.Pad(30));
                WriteToConsole(" {0}", ConsoleColor.Magenta, false, a.MbzArtistId.Pad(8));
                WriteToConsole(" {0}", ConsoleColor.Magenta, false, a.MbzReleaseGroupId.Pad(8));
                System.Console.WriteLine();
            }
            System.Console.WriteLine();
            WriteToConsole("page {0}", ConsoleColor.Blue, true, pageNum + 1);
        }

        private static void ReadInput()
        {
            switch (System.Console.ReadKey().Key)
            {
                case ConsoleKey.Q:
                case ConsoleKey.Escape:
                    quit = true;
                    break;
                case ConsoleKey.A:
                    onlyMissingArtists = !onlyMissingArtists;
                    break;
                case ConsoleKey.L:
                    onlyMissingAlbums = !onlyMissingAlbums;
                    break;
                case ConsoleKey.DownArrow:
                    pageNum++;
                    break;
                case ConsoleKey.UpArrow:
                    if (!IsFirstPage)
                        pageNum--;
                    break;
                case ConsoleKey.R:
                    ReloadAlbums();
                    break;
            }
        }

        private static void ReloadAlbums()
        {
            albums = T3kAlbumService.Read();
        }

        private static void WriteToConsole(string format, ConsoleColor color, bool newLine, params object[] arg)
        {
            System.Console.ForegroundColor = color;
            System.Console.Write(format, arg);
            if (newLine)
                System.Console.WriteLine();
        }
    }

    public static class StringExt
    {
        public static string Pad(this string s, int i)
        {
            if (string.IsNullOrEmpty(s))
                return " ".PadRight(i).Substring(0, i);
            else
                return s.PadRight(i).Substring(0, i);
        }
    }
}
