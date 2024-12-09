namespace TrendLoop.Web.ViewModels.User
{
    public class UserProductViewModel
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string BrandName { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string SubcategoryName { get; set; } = null!;

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
