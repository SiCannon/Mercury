using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Rema.Domain.Entity;

namespace Rema.Extractor.Test
{
    class TestDeserialization
    {
        public static void TestSong()
        {
            string songFile = @"c:\temp\songs.xml";

            List<Song> songs;

            XmlSerializer reader = new XmlSerializer(typeof(List<Song>));
            using (StreamReader file = new StreamReader(songFile))
            {
                songs = (List<Song>)reader.Deserialize(file);
            }

            foreach (var s in songs)
                Console.WriteLine(s.Title);
        }

        public static void TestProduct()
        {
            string songFile = @"c:\temp\products.xml";

            List<Product> products;

            XmlSerializer reader = new XmlSerializer(typeof(List<Product>));
            using (StreamReader file = new StreamReader(songFile))
            {
                products = (List<Product>)reader.Deserialize(file);
            }

            foreach (var p in products.Where(p => !p.ReleaseDate.HasValue).Take(100))
                Console.WriteLine(p.Title);
        }

    }
}
