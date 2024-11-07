using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrendLoop.Data.Models
{
    public class CategoryAttributeType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public int AttributeTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(AttributeTypeId))]
        public AttributeType AttributeType { get; set; } = null!;
    }
}
