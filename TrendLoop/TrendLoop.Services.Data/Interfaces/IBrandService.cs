using TrendLoop.Web.ViewModels.Product;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandInfoViewModel>> GetAllBrandsAsync();
    }
}
