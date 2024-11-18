using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TrendLoop.Data.Models;

namespace TrendLoop.Data.Configuration
{
    public class SubcategoryAttributeTypeConfiguration : IEntityTypeConfiguration<SubcategoryAttributeType>
    {
        public void Configure(EntityTypeBuilder<SubcategoryAttributeType> builder)
        {
            // configure composite primary key
            builder.HasKey(sa => new { sa.SubcategoryId, sa.AttributeTypeId });

            // set the Category to Product relationship and delete behavior
            builder
                .HasOne(sa => sa.Subcategory)
                .WithMany(c => c.SubcategoryAttributeTypes)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
