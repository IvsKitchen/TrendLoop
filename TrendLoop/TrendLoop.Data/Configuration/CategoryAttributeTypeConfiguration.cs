using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TrendLoop.Data.Models;

namespace TrendLoop.Data.Configuration
{
    public class CategoryAttributeTypeConfiguration : IEntityTypeConfiguration<CategoryAttributeType>
    {
        public void Configure(EntityTypeBuilder<CategoryAttributeType> builder)
        {
            // configure composite primary key
            builder.HasKey(ca => new { ca.CategoryId, ca.AttributeTypeId });

            // set the one Category to Product relationship and delete behavior
            builder
                .HasOne(ca => ca.Category)
                .WithMany(c => c.AttributeTypes)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
