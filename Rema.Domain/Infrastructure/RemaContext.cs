using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Rema.Domain.Entity;

namespace Rema.Domain.Infrastructure
{
    /// <remarks>
    /// This does not work.
    /// </remarks>
    public class RemaContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }

        public RemaContext()
        {
            Database.SetInitializer<RemaContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Song>().ToTable("SONGS");
            modelBuilder.Entity<Song>().Property(p => p.Site).HasColumnName("SONGS_SITE");
            modelBuilder.Entity<Song>().Property(p => p.Code).HasColumnName("SONGS_CODE");
            modelBuilder.Entity<Song>().Property(p => p.Title).HasColumnName("SONGS_TITLE");
        }
    }
}
