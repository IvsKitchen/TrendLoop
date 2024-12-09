using System.ComponentModel.DataAnnotations;
using static TrendLoop.Common.EntityValidationConstants.AttributeType;
namespace TrendLoop.Web.ViewModels.Product
{
    public class AttributeTypeInfoViewModel
    {
        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public int SubcategoryId { get; set; }

        public IEnumerable<AttributeValueInfoViewModel> AttributeValues { get; set; } = new HashSet<AttributeValueInfoViewModel>();
    }
}