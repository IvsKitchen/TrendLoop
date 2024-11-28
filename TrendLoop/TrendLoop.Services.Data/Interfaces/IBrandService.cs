using TrendLoop.Web.ViewModels;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandInfoViewModel>> GetAllBrandsAsync();
    }
}
