using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Hub.Domain.Entity;

namespace Hub.Domain.Infrastructure
{
    public class HubContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Artist>().ToTable("Artist", schemaName: "hub");
            modelBuilder.Entity<Product>().ToTable("Product", schemaName: "hub");
        }
    }
}
