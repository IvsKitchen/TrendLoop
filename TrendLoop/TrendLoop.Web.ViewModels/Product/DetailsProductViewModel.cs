namespace TrendLoop.Web.ViewModels.Product
{
    public class DetailsProductViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Price { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string AddedOn { get; set; } = null!;

        public string BrandName { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string SubcategoryName { get; set; } = null!;

        public string SellerAvatarUrl { get; set; } = null!;

        public string SellerName { get; set; } = null!;

        public double SellerRating { get; set; }

        public IEnumerable<AttributeTypeAttributeValueInfoViewModel> AttributeTypesWithValues { get; set; } = new HashSet<AttributeTypeAttributeValueInfoViewModel>();
    }
}
