using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrendLoop.Data.Models
{
    public class ProductSeller
    {
        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [Required]
        public Guid SellerId { get; set; }

        [ForeignKey(nameof(SellerId))]
        public IdentityUser Seller { get; set; } = null!;
    }
}