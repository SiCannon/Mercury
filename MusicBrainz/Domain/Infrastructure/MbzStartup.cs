using System.Data.Entity;

namespace MusicBrainz.Domain.Infrastructure
{
    public static class MbzStartup
    {
        public static void Go()
        {
            Database.SetInitializer<MbzContext>(new MbzDatabaseInitializer());
        }
    }
}
