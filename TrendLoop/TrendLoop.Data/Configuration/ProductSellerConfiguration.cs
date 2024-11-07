using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TrendLoop.Data.Models;

namespace TrendLoop.Data.Configuration
{
    public class ProductSellerConfiguration : IEntityTypeConfiguration<ProductSeller>
    {
        public void Configure(EntityTypeBuilder<ProductSeller> builder)
        {
            // configure composite primary key
            builder.HasKey(pb => new { pb.ProductId, pb.SellerId });
            
            // TODO introduce ApplicationUser and configurate relationship
        }
    }
}
