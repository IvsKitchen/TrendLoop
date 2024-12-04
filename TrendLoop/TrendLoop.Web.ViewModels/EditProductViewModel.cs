using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static TrendLoop.Common.EntityValidationConstants.Product;
namespace TrendLoop.Web.ViewModels
{
    public class EditProductViewModel
    {
        public string Id { get; set; } = null!;

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), PriceMinValueAsString, PriceMaxValueAsString)]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public IFormFile? ImageFile{ get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SubcategoryId { get; set; }

        // Stores current product Attribute Types with their values
        public IEnumerable<AttributeTypeAttributeValueInfoViewModel> CurrentAttributeTypesWithValues { get; set; } = new HashSet<AttributeTypeAttributeValueInfoViewModel>();

        // Stores selected from user product Attribute Types with their values
        [Required]
        public Dictionary<int, int> NewAttributeTypeIdAttributeValueIdPairs { get; set; } = new Dictionary<int, int>();

        public IEnumerable<BrandInfoViewModel> Brands = new HashSet<BrandInfoViewModel>();

        public IEnumerable<CategoryInfoViewModel> Categories = new HashSet<CategoryInfoViewModel>();
    }
}
