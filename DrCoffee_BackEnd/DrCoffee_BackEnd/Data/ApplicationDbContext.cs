using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DrCoffee_BackEnd.Models;

namespace DrCoffee_BackEnd.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductPrice> ProductPrices { get; set; }
    public DbSet<ProductTag> ProductTags { get; set; }
    public DbSet<ProductFlavor> ProductFlavors { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomizationOption> CustomizationOptions { get; set; }
    public DbSet<ProductCustomizationOption> ProductCustomizationOptions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure Product
        builder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.ProductCode).IsUnique();
            entity.HasOne(p => p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure ProductPrice
        builder.Entity<ProductPrice>(entity =>
        {
            entity.HasIndex(e => new { e.ProductId, e.Size }).IsUnique();
            entity.HasOne(pp => pp.Product)
                  .WithMany(p => p.ProductPrices)
                  .HasForeignKey(pp => pp.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure ProductTag
        builder.Entity<ProductTag>(entity =>
        {
            entity.HasIndex(e => new { e.ProductId, e.Tag }).IsUnique();
            entity.HasOne(pt => pt.Product)
                  .WithMany(p => p.ProductTags)
                  .HasForeignKey(pt => pt.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure ProductFlavor
        builder.Entity<ProductFlavor>(entity =>
        {
            entity.HasIndex(e => new { e.ProductId, e.FlavorName }).IsUnique();
            entity.HasOne(pf => pf.Product)
                  .WithMany(p => p.ProductFlavors)
                  .HasForeignKey(pf => pf.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Order
        builder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.OrderNumber).IsUnique();
            entity.HasOne(o => o.Customer)
                  .WithMany(c => c.Orders)
                  .HasForeignKey(o => o.CustomerId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure OrderItem
        builder.Entity<OrderItem>(entity =>
        {
            entity.HasOne(oi => oi.Order)
                  .WithMany(o => o.OrderItems)
                  .HasForeignKey(oi => oi.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(oi => oi.Product)
                  .WithMany()
                  .HasForeignKey(oi => oi.ProductId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Category
        builder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
        });
    }
}



