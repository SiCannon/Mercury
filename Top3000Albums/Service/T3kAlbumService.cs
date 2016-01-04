using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Top3000Albums.Entity;

namespace Top3000Albums.Service
{
    public class T3kAlbumService
    {
        const string DefaultFilename = @".\Data\Top3000Albums.xml";

        public static List<Album> Read(string filename = DefaultFilename)
        {
            XmlSerializer reader = new XmlSerializer(typeof(List<Album>));
            using (StreamReader file = new StreamReader(filename, Encoding.GetEncoding(1252)))
            {
                return (List<Album>)reader.Deserialize(file);
            }
        }

        public static List<string> ListDistinctArtistNames()
        {
            return Read()
                .Select(a => a.Artist)
                .Distinct()
                .ToList();
        }

        public static void Write(List<Album> albums, string filename = DefaultFilename)
        {
            var writer = new XmlSerializer(albums.GetType());
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            using (var file = new StreamWriter(filename, false, Encoding.GetEncoding(1252)))
            {
                writer.Serialize(file, albums, ns);
            }
        }

        public static void AddAlbumIds(string filename = DefaultFilename)
        {
            var albums = Read(filename);
            AddAlbumIds(albums);
            Write(albums, filename);
        }

        public static void AddAlbumIds(List<Album> albums)
        {
            int id = 1;
            foreach (var a in albums)
            {
                if (!a.AlbumIdAsInteger.HasValue)
                    a.AlbumIdAsInteger = id++;
            }
        }
    }
}
