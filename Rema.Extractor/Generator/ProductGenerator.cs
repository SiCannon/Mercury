using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Rema.Domain.Entity;
using Rema.Domain.Service.Concrete;

namespace Rema.Extractor.Generator
{
    class ProductGenerator
    {
        public void Generate(string filename)
        {
            var service = new ProductService();
            var products = service.ListBySite("BGUK");

            XmlSerializer writer = new XmlSerializer(typeof(List<Product>));
            StreamWriter file = new StreamWriter(filename);
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            writer.Serialize(file, products, ns);
            file.Close();

            Console.WriteLine("Wrote {0} products", products.Count);
        }
    }
}
