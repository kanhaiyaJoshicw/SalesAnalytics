using Microsoft.EntityFrameworkCore;
using SalesAnalytics.Core.Entities;

namespace SalesAnalytics.Infrastructure
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) { }

        public DbSet<SaleRecord> SaleRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entity properties, relationships, etc.
            modelBuilder.Entity<SaleRecord>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Region).IsRequired().HasMaxLength(50);
            });
        }
    }
}
