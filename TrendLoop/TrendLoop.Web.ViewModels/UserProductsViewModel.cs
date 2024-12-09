namespace TrendLoop.Web.ViewModels
{
    public class UserProductsViewModel
    {
        public IEnumerable<UserProductViewModel> BoughtProducts { get; set; }
        public IEnumerable<UserProductViewModel> ProductsForSale { get; set; }
    }
}
