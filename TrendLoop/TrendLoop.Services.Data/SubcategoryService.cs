using Microsoft.EntityFrameworkCore;
using TrendLoop.Data.Models;
using TrendLoop.Data.Repository.Interfaces;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels.Product;

namespace TrendLoop.Services.Data
{
    public class SubcategoryService : BaseService, ISubcategoryService
    {
        private readonly IRepository<Category, int> categoryRepository;
        private readonly IRepository<Subcategory, int> subcategoryRepository;

        public SubcategoryService(IRepository<Subcategory, int> subcategoryRepository)
        {
            this.subcategoryRepository = subcategoryRepository;
        }

        public async Task<IEnumerable<SubcategoryInfoViewModel>> GetAllSubcategoriesAsync()
        {
            return await subcategoryRepository
                .GetAllAttached()
                .Where(c => !c.IsDeleted)
                .Select(c => new SubcategoryInfoViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<SubcategoryInfoViewModel>> GetSubcategoriesByCategoryIdAsync(int categoryId)
        {
            return await subcategoryRepository
                .GetAllAttached()
                .Where(sc => !sc.IsDeleted && sc.CategoryId == categoryId)
                .Select(sc => new SubcategoryInfoViewModel
                {
                    Id = sc.Id,
                    Name = sc.Name,
                })
                .ToListAsync();
        }
    }
}
