using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MusicBrainz.Domain.Entity;

namespace MusicBrainz.Domain.Infrastructure
{
    class MbzContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Artist>().ToTable("Artist", "mbz");
        }
    }
}
