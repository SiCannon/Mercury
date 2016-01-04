using System.Data.Entity;
using Hub.Domain.Infrastructure.Seed;

namespace Hub.Domain.Infrastructure
{
    //class HubDatabaseInitializer : DropCreateDatabaseAlways<HubContext>
    class HubDatabaseInitializer : DropCreateDatabaseIfModelChanges<HubContext>
    {
        protected override void Seed(HubContext context)
        {
            //context.Database.ExecuteSqlCommand("create unique index idxu_Artist_Name on Artist (Name)");
            //ArtistSeeder.Seed(context, @"c:\temp\artist.xml");
            
        }
    }
}
