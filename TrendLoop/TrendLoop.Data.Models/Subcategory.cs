using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TrendLoop.Common.EntityValidationConstants.Subcategory;

namespace TrendLoop.Data.Models
{
    public class Subcategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
        
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

        public virtual ICollection<SubcategoryAttributeType> SubcategoryAttributeTypes { get; set; } = new HashSet<SubcategoryAttributeType>();
    }
}