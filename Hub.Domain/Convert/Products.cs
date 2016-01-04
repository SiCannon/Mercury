using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Hub.Domain.Entity;
using Hub.Domain.Infrastructure;
using MusicBrainz.WebService.Service;
using Top3000Albums.Service;

namespace Hub.Domain.Convert
{
    public static class Products
    {
        public static void ImportT3k()
        {
            ImportT3k(40);
        }

        public static void ImportT3k(int count = -1)
        {
            var ctx = new HubContext();

            var albums = T3kAlbumService.Read();
            int howMany = count == -1 ? albums.Count : count > albums.Count ? albums.Count : count;
            for (int i = 0; i < howMany; i++)
            {
                if (albums[i].MbzReleaseGroupIdAsGuid.HasValue)
                {
                    var releaseGroup = ReleaseGroupWebService.Query(albums[i].MbzReleaseGroupIdAsGuid.Value);
                    if (releaseGroup.Releases.Count > 0)
                    {
                        var release = ReleaseWebService.Query(releaseGroup.Releases.ElementAt(0).ReleaseId);
                        ctx.Products.Add(new Product { Title = release.Title });
                        Console.WriteLine("added {0}", release.Title);
                    }
                }
            }

            ctx.SaveChanges();
        }

        public static void SaveProductsToXml(string filename)
        {
            var products = (new HubContext()).Products.ToList();
            var s = new XmlSerializer(products.GetType());
            var writer = new StreamWriter(filename);
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            s.Serialize(writer, products, ns);
            writer.Close();
        }

        public static void LoadProductsFromXml()
        {
            LoadProductsFromXml(@"c:\temp\products.xml");
        }

        public static void LoadProductsFromXml(string filename)
        {
            var ctx = new HubContext();

            ctx.Products.ToList().ForEach(p => ctx.Products.Remove(p));

            var ser = new XmlSerializer(typeof(List<Product>));
            var rdr = new StreamReader(filename);
            var products = (List<Product>)ser.Deserialize(rdr);
            foreach (var p in products)
            {
                ctx.Products.Add(p);
            }

            ctx.SaveChanges();
        }
    }
}
