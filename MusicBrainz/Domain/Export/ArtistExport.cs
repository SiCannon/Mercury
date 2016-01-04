using System;
using System.Collections.Generic;
using System.Linq;
using MusicBrainz.Domain.Entity;
using MusicBrainz.Domain.Service;

namespace MusicBrainz.Domain.Export
{
    public class ArtistExport
    {
        public static void Export(string filename)
        {
            Console.WriteLine("exporting artists...");
            var artists = (new ArtistService()).Artists.ToList();
            Helpers.Xml.Serialize(artists, filename);
        }

        public static void Import(string filename)
        {
            Console.WriteLine("importing artists...");
            var artists = Helpers.Xml.DeserializePlainFromFile<List<Artist>>(filename);
            (new ArtistService()).SaveMultipleNew(artists);
        }
    }
}
