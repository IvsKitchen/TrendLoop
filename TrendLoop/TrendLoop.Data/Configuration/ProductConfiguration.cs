using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrendLoop.Data.Models;

namespace TrendLoop.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // set the ApplicationUser as Seller and Product relationship and delete behavior
            builder
                .HasOne(p => p.Seller)
                .WithMany(s => s.ProductsForSale)
                .OnDelete(DeleteBehavior.Restrict);

            // set the ApplicationUser as Buyer and Product relationship and delete behavior
            builder
                .HasOne(p => p.Buyer)
                .WithMany(b => b.ProductsBought)
                .OnDelete(DeleteBehavior.Restrict);

            // set the ApplicationUser as Buyer and Product relationship and delete behavior
            builder
                .HasOne(p => p.Subcategory)
                .WithMany(s => s.Products)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
