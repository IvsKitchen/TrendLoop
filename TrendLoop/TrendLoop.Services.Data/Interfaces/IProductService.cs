using TrendLoop.Web.ViewModels.Product;
using TrendLoop.Web.ViewModels.User;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<AllProductsIndexViewModel>> GetAllProductsAsync();

        Task<DetailsProductViewModel> GetProductDetailsAsync(Guid productId);

        Task<bool> AddProductAsync(Guid sellerId, AddProductViewModel model);

        Task<EditProductViewModel?> GetProductToEditAsync(Guid id);

        Task<bool> EditProductAsync(Guid productId, EditProductViewModel model);

        Task<DeleteProductViewModel?> GetProductForDeleteByIdAsync(Guid id);

        Task<bool> SoftDeleteProductAsync(Guid id);

        Task<IEnumerable<UserProductViewModel>> GetBoughtProductsByUserAsync(Guid userId);

        Task<IEnumerable<UserProductViewModel>> GetSelledProductsByUserAsync(Guid userId);
    }
}
