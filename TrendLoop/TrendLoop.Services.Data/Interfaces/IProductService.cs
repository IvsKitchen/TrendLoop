using TrendLoop.Web.ViewModels;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<AllProductsIndexViewModel>> GetAllProductsAsync();

        Task<ProductDetailsViewModel> GetProductDetailsAsync(Guid productId);

        Task<bool> AddProductAsync(Guid sellerId, AddProductViewModel model);

    }
}
