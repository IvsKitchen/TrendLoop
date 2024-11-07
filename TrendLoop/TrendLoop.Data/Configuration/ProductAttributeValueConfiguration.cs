using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TrendLoop.Data.Models;

namespace TrendLoop.Data.Configuration
{
    public class ProductAttributeValueConfiguration : IEntityTypeConfiguration<ProductAttributeValue>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
        {
            // configure composite primary key
            builder.HasKey(pav => new { pav.ProductId, pav.AttributeValueId });

            // set the Category to Product relationship and delete behavior
            builder
                .HasOne(pav => pav.Product)
                .WithMany(p => p.ProductAttributeValues)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
