using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrendLoop.Data.Models;

namespace TrendLoop.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Set the ApplicationUser as Seller - Product relationship and delete behavior
            builder
                .HasOne(p => p.Seller)
                .WithMany(s => s.ProductsForSale)
                .OnDelete(DeleteBehavior.Restrict);

            // Set the ApplicationUser as Buyer - Product relationship and delete behavior
            builder
                .HasOne(p => p.Buyer)
                .WithMany(b => b.ProductsBought)
                .OnDelete(DeleteBehavior.Restrict);

            // Set Product and Subcategory relationship and delete behavior
            builder
                .HasOne(p => p.Subcategory)
                .WithMany(s => s.Products)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
