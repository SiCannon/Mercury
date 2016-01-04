using System;
using Rema.Domain.Service.Concrete;
using Rema.Extractor.Generator;

namespace Rema.Extractor
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"c:\temp\remadata";

            //TestSongService.TestListBySite();
            //TestDeserialization.TestProduct();

            //(new SongGenerator()).Generate(@"c:\temp\songs.xml");
            //(new ProductGenerator()).Generate(@"c:\temp\products.xml");


            /*AnyGenerator.Generate(path, "company", (new CompanyService()).ListAll());
            AnyGenerator.Generate(path, "label", (new LabelService()).ListAll());
            AnyGenerator.Generate(path, "configuration", (new ConfigService()).ListAll());
            AnyGenerator.Generate(path, "song", (new SongService()).ListBySite("BGUK", false));*/
            //AnyGenerator.Generate(path, "product", (new ProductService()).ListBySite("BGUK"));
            //AnyGenerator.Generate(path, "recording", (new RecordingService()).ListBySite("BGUK"));
            AnyGenerator.Generate(path, "product", (new ProductService()).ListBySite("BGUK", 1000));
            //AnyGenerator.Generate(path, "track", (new TrackService()).ListByProductSite("BGUK"));

            Console.WriteLine();
            Console.WriteLine("press any key to exit...");
            Console.ReadKey();
        }
    }
}
