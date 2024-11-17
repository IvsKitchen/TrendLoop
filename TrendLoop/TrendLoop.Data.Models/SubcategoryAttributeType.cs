using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrendLoop.Data.Models
{
    public class SubcategoryAttributeType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SubcategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(SubcategoryId))]
        public Subcategory Subcategory { get; set; } = null!;

        [Required]
        public int AttributeTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(AttributeTypeId))]
        public AttributeType AttributeType { get; set; } = null!;
    }
}
