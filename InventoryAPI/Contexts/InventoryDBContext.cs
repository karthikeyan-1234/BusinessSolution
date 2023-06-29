
using CommonLibrary.Models;

using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Contexts
{
    public class InventoryDBContext : DbContext
    {
        public DbSet<Inventory>? Inventories { get; set; }

        public InventoryDBContext(DbContextOptions<InventoryDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var inventories = modelBuilder.Entity<Inventory>();
            inventories.HasKey(i => i.id);
            inventories.Property(i => i.id).UseIdentityColumn();

            base.OnModelCreating(modelBuilder);
        }
    }
}
