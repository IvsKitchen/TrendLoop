using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TrendLoop.Common.EntityValidationConstants.AttributeValue;

namespace TrendLoop.Data.Models
{
    public class AttributeValue
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValueMaxLength)]
        public string Value { get; set; } = null!;

        [Required]
        public int AttributeTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(AttributeTypeId))]
        public AttributeType AttributeType { get; set; } = null!;
    }
}
