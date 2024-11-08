using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrendLoop.Data.Models
{
    public class ProductAttributeValue
    {
        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [Required]
        public int AttributeValueId { get; set; }

        [ForeignKey(nameof(AttributeValueId))]
        public AttributeValue AttributeValue { get; set; } = null!;
    }
}
