using System.ComponentModel.DataAnnotations;
using static TrendLoop.Common.EntityValidationConstants.Subcategory;
namespace TrendLoop.Web.ViewModels
{
    public class SubcategoryInfoViewModel
    {
        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }
    }
}