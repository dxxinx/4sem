using System.Data.Entity;
using FlowerShop.Models;

namespace FlowerShop.Data
{
    public class FlowerShopContext : DbContext
    {
        public FlowerShopContext()
            : base("name=FlowerShopDb")
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<ProductAudit> ProductAudit { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .HasColumnName("ProductUid");

            modelBuilder.Entity<Product>()
                .Property(p => p.ShortName)
                .IsRequired()
                .HasMaxLength(120);

            modelBuilder.Entity<Product>()
                .Property(p => p.FullName)
                .IsRequired()
                .HasMaxLength(250);

            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(1000);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.DiscountPercent)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Product>()
                .HasRequired(p => p.CategoryEntity)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasRequired(p => p.ManufacturerEntity)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.ManufacturerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Manufacturer>()
                .HasIndex(m => m.Name)
                .IsUnique();

            modelBuilder.Entity<ProductAudit>()
                .ToTable("ProductAudit");

            modelBuilder.Entity<ProductAudit>()
                .Property(a => a.OldPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ProductAudit>()
                .Property(a => a.NewPrice)
                .HasPrecision(10, 2);
        }
    }
}
