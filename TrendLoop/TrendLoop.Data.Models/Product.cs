using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TrendLoop.Common.EntityValidationConstants.Product;
namespace TrendLoop.Data.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), PriceMinValueAsString, PriceMaxValueAsString)]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public DateTime AddedOn { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public int BrandId { get; set; }

        [Required]
        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; } = null!;

        [Required]
        public int SubcategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(SubcategoryId))]
        public Subcategory Subcategory { get; set; } = null!;

        [Required]
        public Guid SellerId { get; set; }

        [Required]
        [ForeignKey(nameof(SellerId))]
        public ApplicationUser Seller { get; set; } = null!;

        public Guid? BuyerId { get; set; }

        [ForeignKey(nameof(BuyerId))]
        public ApplicationUser? Buyer { get; set; }

        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new HashSet<ProductAttributeValue>();

        public bool IsDeleted { get; set; } = false;
    }
}
