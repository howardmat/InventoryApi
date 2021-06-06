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
        public DbSet<Formula> Formula { get; set; }
        public DbSet<FormulaIngredient> FormulaIngredient { get; set; }
        public DbSet<Tenant> Tenant { get; set; }
        public DbSet<UnitOfMeasurement> UnitOfMeasurement { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Model Configuration
            builder.Entity<Address>(e =>
            {
                e.HasOne(u => u.Province)
                    .WithMany()
                    .HasForeignKey(u => new { u.ProvinceIsoCode, u.CountryIsoCode })
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(u => u.Country)
                    .WithMany()
                    .HasForeignKey(u => u.CountryIsoCode)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<FormulaIngredient>(e =>
            {
                e.Property(f => f.Quantity).HasPrecision(19, 4);
                e.HasOne(u => u.Material)
                    .WithMany()
                    .HasForeignKey(u => u.MaterialId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Material>(e =>
            {
                e.HasOne(u => u.Tenant)
                    .WithMany()
                    .HasForeignKey(u => u.TenantId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<MaterialInventoryTransaction>(e => 
            {
                e.Property(f => f.AmountPaid).HasPrecision(19, 4);
                e.Property(f => f.Quantity).HasPrecision(19, 4);

                e.HasOne(m => m.Tenant)
                    .WithMany()
                    .HasForeignKey(m => m.TenantId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Product>(e =>
            {
                e.Property(f => f.Price).HasPrecision(19, 4);
            });

            builder.Entity<ProductInventoryTransaction>(e =>
            {
                e.Property(f => f.Quantity).HasPrecision(19, 4);
            });

            builder.Entity<Province>(e =>
            {
                e.HasKey(p => new { p.IsoCode, p.CountryIsoCode });

                e.HasOne(p => p.Country)
                    .WithMany()
                    .HasForeignKey(u => u.CountryIsoCode)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Tenant>(e =>
            {
                e.HasOne(u => u.OwnerUser)
                    .WithMany()
                    .HasForeignKey(u => u.OwnerUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(u => u.PrimaryAddress)
                    .WithMany()
                    .HasForeignKey(u => u.PrimaryAddressId);
            });

            builder.Entity<UserProfile>(e =>
            {
                e.ToTable("UserProfile");

                e.HasOne(u => u.Tenant)
                    .WithMany(t => t.Users)
                    .HasForeignKey(u => u.TenantId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            #endregion

            #region Seed Data
            builder.Entity<UnitOfMeasurement>().HasData(UnitOfMeasurementSeed.Data);
            #endregion
        }
    }
}
