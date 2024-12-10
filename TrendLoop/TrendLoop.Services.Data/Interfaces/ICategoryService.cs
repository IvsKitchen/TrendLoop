using TrendLoop.Web.ViewModels.Product;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryInfoViewModel>> GetAllCategoriesAsync();

        Task<IEnumerable<string>> GetAllCategoriesNames();
    }
}
