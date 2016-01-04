using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Memphis.Database.Entity;

namespace Memphis.Database.Infrastructure
{
    public class MemphisContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<ArtistTag> ArtistTags { get; set; }
        public DbSet<Recording> Recordings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        static MemphisContext()
        {
            System.Data.Entity.Database.SetInitializer<MemphisContext>(new MemphisDatabaseInitializer_WhenChanged());
            //System.Data.Entity.Database.SetInitializer<MemphisContext>(new MemphisDatabaseInitializer_Always());
            //System.Data.Entity.Database.SetInitializer<MemphisContext>(null);
        }
    }
}
