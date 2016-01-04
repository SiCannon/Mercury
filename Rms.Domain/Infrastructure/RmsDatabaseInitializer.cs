using System;
using System.Data.Entity;
using Rms.Domain.Entity;
using Rms.Domain.Seed;

namespace Rms.Domain.Infrastructure
{
    class RmsDatabaseInitializer : DropCreateDatabaseIfModelChanges<RmsContext>
    {
        protected override void Seed(RmsContext context)
        {
            //SongSeeder.Seed(context, @"c:\temp\remadata\song.xml");
            Console.WriteLine("seeding products...");
            ProductSeeder.Seed(context, @"c:\temp\remadata\product.xml");
        }
    }
}
