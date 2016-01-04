using System.Data.Entity;

namespace Hub.Domain.Infrastructure
{
    public static class HubStartup
    {
        public static void Go()
        {
            Database.SetInitializer<HubContext>(new HubDatabaseInitializer());
        }
    }
}
