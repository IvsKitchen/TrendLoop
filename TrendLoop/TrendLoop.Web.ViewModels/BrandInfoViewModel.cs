using System.ComponentModel.DataAnnotations;
using static TrendLoop.Common.EntityValidationConstants.Brand;
namespace TrendLoop.Web.ViewModels
{
    public class BrandInfoViewModel
    {
        public int Id { get; set; }

        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}