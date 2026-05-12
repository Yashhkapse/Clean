using Microsoft.EntityFrameworkCore;
using ProCleanArchitecture.Domain.Entities;

namespace ProCleanArchitecture.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Tables
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ======================
        // PRODUCT CONFIG
        // ======================
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(p => p.Description)
                  .HasMaxLength(1000);

            // FIX FOR DECIMAL WARNING
            entity.Property(p => p.Price)
                  .HasColumnType("decimal(18,2)");

            entity.HasOne(p => p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ======================
        // CATEGORY CONFIG
        // ======================
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                  .IsRequired()
                  .HasMaxLength(150);

            entity.HasData(CategorySeedData.Categories);
        });

        // ======================
        // USER CONFIG
        // ======================
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.FirstName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(u => u.LastName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(u => u.Email)
                  .IsRequired()
                  .HasMaxLength(256);

            entity.HasIndex(u => u.Email)
                  .IsUnique();

            entity.Property(u => u.Role)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(u => u.IsActive)
                  .HasDefaultValue(true);

            entity.Property(u => u.CreatedAtUtc)
                  .IsRequired();
        });
    }
}
