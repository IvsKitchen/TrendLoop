using System.ComponentModel.DataAnnotations;
using static TrendLoop.Common.EntityValidationConstants.Category;
namespace TrendLoop.Web.ViewModels
{
    public class CategoryInfoViewModel
    {
        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public IEnumerable<SubcategoryInfoViewModel> Subcategories { get; set; } = new HashSet<SubcategoryInfoViewModel>();
    }
}