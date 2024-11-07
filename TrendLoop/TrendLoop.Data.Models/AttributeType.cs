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

        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
    }
}