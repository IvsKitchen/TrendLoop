using TrendLoop.Web.ViewModels.Product;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface ISubcategoryService
    {
        Task<IEnumerable<SubcategoryInfoViewModel>> GetAllSubcategoriesAsync();

        Task<IEnumerable<SubcategoryInfoViewModel>> GetSubcategoriesByCategoryIdAsync(int categoryId);
    }
}
