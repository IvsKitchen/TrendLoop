using TrendLoop.Web.ViewModels.Product;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface IPurchaseService
    {
        Task<BuyProductViewModel> GetProductToBuyAsync(Guid productId);

        Task<bool> ExecuteBuyProductAsync(Guid productId, Guid buyerId);
    }
}
