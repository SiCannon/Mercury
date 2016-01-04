using System.Data.Entity;

namespace Rms.Domain.Infrastructure
{
    public static class RmsStartup
    {
        public static void Go()
        {
            Database.SetInitializer<RmsContext>(new RmsDatabaseInitializer());
        }
    }
}
