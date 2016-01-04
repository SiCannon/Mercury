using System.Data.Entity;

namespace Memphis.Database.Infrastructure
{
    class MemphisDatabaseInitializer_Always : DropCreateDatabaseAlways<MemphisContext>
    {
        protected override void Seed(MemphisContext context)
        {

        }
    }

    class MemphisDatabaseInitializer_WhenChanged : DropCreateDatabaseIfModelChanges<MemphisContext>
    {
        protected override void Seed(MemphisContext context)
        {

        }
    }

}
