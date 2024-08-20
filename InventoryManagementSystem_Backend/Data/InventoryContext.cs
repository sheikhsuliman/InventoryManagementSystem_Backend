using InventoryManagementSystem_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem_Backend.Data
{
    public class InventoryContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options) { }
    }
}
