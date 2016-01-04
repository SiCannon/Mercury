using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MusicBrainz.QueryResultCache
{
    class MbzqContext : DbContext
    {
        public DbSet<Query> Queries { get; set; }

        public MbzqContext()
        {
            Database.SetInitializer<MbzqContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
