using TrendLoop.Web.ViewModels;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface IProductService : IBaseService
    {
        Task<IEnumerable<AllProductsIndexViewModel>> GetAllProductsAsync();
    }
}
