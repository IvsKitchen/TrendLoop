using TrendLoop.Web.ViewModels;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryInfoViewModel>> GetAllCategoriesAsync();
    }
}
