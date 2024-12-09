using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels.Product;

namespace TrendLoop.Services.Data
{
    public class PurchaseService : BaseService, IPurchaseService
    {
        IProductService productService;

        public PurchaseService(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<BuyProductViewModel> GetProductToBuyAsync(Guid productId)
        {
            return await productService.GetProductToBuyAsync(productId);
        }

        public async Task<bool> ExecuteBuyProductAsync(Guid productId, Guid buyerId)
        {
            return await productService.BuyProductAsync(productId, buyerId);
        }
    }
}
