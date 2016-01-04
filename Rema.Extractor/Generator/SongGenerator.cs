using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Rema.Domain.Entity;
using Rema.Domain.Service.Concrete;

namespace Rema.Extractor.Generator
{
    class SongGenerator
    {
        public void Generate(string filename)
        {
            var service = new SongService();
            var songs = service.ListBySite("BGUK", true, 1000);

            XmlSerializer writer = new XmlSerializer(typeof(List<Song>));
            StreamWriter file = new StreamWriter(filename);
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            writer.Serialize(file, songs, ns);
            file.Close();

            Console.WriteLine("Wrote {0} songs", songs.Count);
        }
    }
}
