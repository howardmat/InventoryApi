using Data.Models;
using Data.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<Address> Address { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<MaterialInventoryTransaction> MaterialInventoryTransaction { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductInventoryTransaction> ProductInventoryTransaction { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Formula> Recipe { get; set; }
        public DbSet<FormulaIngredient> RecipeIngredient { get; set; }
        public DbSet<Tenant> Tenant { get; set; }
        public DbSet<UnitOfMeasurement> UnitOfMeasurement { get; set; }
        public DbSet<User> User { get; set; }

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Model Configuration
            
            #endregion

            #region Seed Data
            builder.Entity<UnitOfMeasurement>().HasData(UnitOfMeasurementSeed.Data);
            #endregion
        }
    }
}
