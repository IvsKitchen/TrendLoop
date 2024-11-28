using System.ComponentModel.DataAnnotations;

using static TrendLoop.Common.EntityValidationConstants.AttributeType;

namespace TrendLoop.Data.Models
{
    public class AttributeType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<SubcategoryAttributeType> SubcategoryAttributeTypes { get; set; } = new HashSet<SubcategoryAttributeType>();

        public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new HashSet<AttributeValue>();
    }
}