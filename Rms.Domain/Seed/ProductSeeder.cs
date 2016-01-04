using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Rms.Domain.Infrastructure;

namespace Rms.Domain.Seed
{
    using RemaProduct = Rema.Domain.Entity.Product;
    using RmsProduct = Rms.Domain.Entity.Product;

    class ProductSeeder
    {
        public static void Seed(RmsContext context, string ProductFile)
        {
            List<RemaProduct> Products;

            XmlSerializer reader = new XmlSerializer(typeof(List<RemaProduct>));
            using (StreamReader file = new StreamReader(ProductFile))
            {
                Console.WriteLine("loading products...");
                Products = (List<RemaProduct>)reader.Deserialize(file);
                Console.WriteLine("load complete");
            }

            int counter = 0;

            Console.WriteLine("inserting products...");
            foreach (RemaProduct p in Products)
            {
                context.Products.Add(new RmsProduct(p));
                counter++;
                if (counter % 1000 == 0)
                    Console.WriteLine("{0} records inserted", counter);
            }
            Console.WriteLine("insert complete");
        }
    }
}
