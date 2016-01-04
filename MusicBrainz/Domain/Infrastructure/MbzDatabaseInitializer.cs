using System.Data.Entity;

namespace MusicBrainz.Domain.Infrastructure
{
    class MbzDatabaseInitializer : DropCreateDatabaseIfModelChanges<MbzContext>
    //class MbzDatabaseInitializer : DropCreateDatabaseAlways<MbzContext>
    {
        protected override void Seed(MbzContext context)
        {
            System.Console.WriteLine("Seeding MusicBrainz database...");

            //context.Database.ExecuteSqlCommand("create unique index idxu_mbz_Artist_Name on mbz.Artist (Name)");

            Export.ArtistExport.Import(@".\Data\artists.xml");
        }
    }
}
