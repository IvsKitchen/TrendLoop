using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TrendLoop.Data.Models;

namespace TrendLoop.Data.Configuration
{
    public class ProductBuyerConfiguration : IEntityTypeConfiguration<ProductBuyer>
    {
        public void Configure(EntityTypeBuilder<ProductBuyer> builder)
        {
            // configure composite primary key
            builder.HasKey(pb => new { pb.ProductId, pb.BuyerId });

            // TODO introduce ApplicationUser and configurate relationship
        }
    }
}
