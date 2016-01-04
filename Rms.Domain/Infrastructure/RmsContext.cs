using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Rms.Domain.Entity;

namespace Rms.Domain.Infrastructure
{
    class RmsContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Song>().ToTable("Song", schemaName: "rms");
            modelBuilder.Entity<Product>().ToTable("Product", schemaName: "rms");
        }
    }
}
