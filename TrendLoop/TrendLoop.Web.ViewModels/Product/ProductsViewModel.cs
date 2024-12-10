namespace TrendLoop.Web.ViewModels.Product
{
    public class ProductsViewModel
    {
        public string? SearchQuery { get; set; }

        public string? BrandFilter { get; set; }

        public string? CategoryFilter { get; set; }

        public string? SubcategoryFilter { get; set; }

        public IEnumerable<string>? AllBrands { get; set; }

        public IEnumerable<string>? AllCategories { get; set; }

        public IEnumerable<string>? AllSubcategories { get; set; }

        public int? PageNumber { get; set; } = 1;

        public int? PageSize { get; set; } = 15;

        public int? TotalItems { get; set; }

        public int? TotalPages { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; } = new HashSet<ProductViewModel>();
    }
}
