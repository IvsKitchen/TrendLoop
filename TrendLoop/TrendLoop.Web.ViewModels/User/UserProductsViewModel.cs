namespace TrendLoop.Web.ViewModels.User
{
    public class UserProductsViewModel
    {
        public IEnumerable<UserProductViewModel> ProductsForSale { get; set; }
        public IEnumerable<UserProductViewModel> BoughtProducts { get; set; }
    }
}
