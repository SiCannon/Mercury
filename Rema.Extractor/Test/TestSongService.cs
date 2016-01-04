using System;
using Rema.Domain.Entity;
using Rema.Domain.Service.Concrete;

namespace Rema.Extractor.Test
{
    static class TestSongService
    {
        public static void TestGetBySiteAndCode()
        {
            var song = service.GetBySiteAndCode("BGUK", 100);

            if (song != null)
                Console.WriteLine(song.Title);
            else
                Console.WriteLine("song not found");
        }

        public static void TestListBySite()
        {
            var songs = service.ListBySite("BGUK", true, 100);
            foreach (Song s in songs)
                Console.WriteLine(s.Title);
            
        }

        static SongService service = new SongService();
    }
}
