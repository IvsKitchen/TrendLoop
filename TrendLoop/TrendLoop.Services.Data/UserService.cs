using Microsoft.EntityFrameworkCore;
using TrendLoop.Data.Models;
using TrendLoop.Data.Repository.Interfaces;
using TrendLoop.Services.Data.Interfaces;

namespace TrendLoop.Services.Data
{
    public class UserService : BaseService, IUserService
    {
        private readonly IRepository<ApplicationUser, Guid> UserRepository;
        
        public UserService(IRepository<ApplicationUser, Guid> UserRepository)
        {
            this.UserRepository = UserRepository;
        }

        public async Task<bool> IsUserProductSeller(Guid userId, Guid productId)
        {
            ApplicationUser user = await UserRepository
                .GetAllAttached()
                .Include(p => p.ProductsForSale)
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();
            
            return user.ProductsForSale.Any(p => p.Id == productId);
        }
    }
}
