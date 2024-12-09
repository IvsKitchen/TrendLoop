using Microsoft.EntityFrameworkCore;
using TrendLoop.Data.Models;
using TrendLoop.Data.Repository.Interfaces;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels.Product;

namespace TrendLoop.Services.Data
{
    public class BrandService : BaseService, IBrandService
    {
        private readonly IRepository<Brand, int> brandRepository;

        public BrandService(IRepository<Brand, int> brandRepository)
        {
            this.brandRepository = brandRepository;
        }

        public async Task<IEnumerable<BrandInfoViewModel>> GetAllBrandsAsync()
        {
            return await brandRepository
                .GetAllAttached()
                .Where(b => !b.IsDeleted)
                .Select(c => new BrandInfoViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }
    }
}
