﻿using TrendLoop.Web.ViewModels.Product;
using TrendLoop.Web.ViewModels.User;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync(ProductsViewModel model);

        Task<int> GetAllProductsCountAsync(ProductsViewModel model);

        Task<DetailsProductViewModel> GetProductDetailsAsync(Guid productId);

        Task<bool> AddProductAsync(Guid sellerId, AddProductViewModel model);

        Task<EditProductViewModel?> GetProductToEditAsync(Guid id);

        Task<bool> EditProductAsync(Guid productId, EditProductViewModel model);

        Task<BuyProductViewModel> GetProductToBuyAsync(Guid productId);

        Task<bool> BuyProductAsync(Guid productId, Guid buyerId);

        Task<DeleteProductViewModel?> GetProductForDeleteByIdAsync(Guid id);

        Task<bool> SoftDeleteProductAsync(Guid id);

        Task<IEnumerable<UserProductViewModel>> GetBoughtProductsByUserAsync(Guid userId);

        Task<IEnumerable<UserProductViewModel>> GetProductsForSaleByUserAsync(Guid userId);
    }
}
