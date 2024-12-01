
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static TrendLoop.Common.EntityValidationConstants.Product;
namespace TrendLoop.Web.ViewModels
{
    public class AddProductViewModel
    {
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

        public IFormFile? ImageFile{ get; set; }

        [Required]
        public DateTime AddedOn { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SubcategoryId { get; set; }

        [Required]
        public Dictionary<int, int> AttributeTypeIdAttributeValueIdPair { get; set; } = new Dictionary<int, int>();

        public IEnumerable<BrandInfoViewModel> Brands = new HashSet<BrandInfoViewModel>();

        public IEnumerable<CategoryInfoViewModel> Categories = new HashSet<CategoryInfoViewModel>();
    }
}
