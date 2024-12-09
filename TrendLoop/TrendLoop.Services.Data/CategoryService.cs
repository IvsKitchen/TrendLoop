using Microsoft.EntityFrameworkCore;
using TrendLoop.Data.Models;
using TrendLoop.Data.Repository.Interfaces;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels;
using TrendLoop.Web.ViewModels.Product;

namespace TrendLoop.Services.Data
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IRepository<Category, int> categoryRepository;

        public CategoryService(IRepository<Category, int> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryInfoViewModel>> GetAllCategoriesAsync()
        {
            return await categoryRepository
                .GetAllAttached()
                .Where(c => !c.IsDeleted)
                .Select(c => new CategoryInfoViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subcategories = c.Subcategories
                        .Select(sc => new SubcategoryInfoViewModel { Id = sc.Id, Name = sc.Name })
                })
                .ToListAsync();
        }
    }
}
