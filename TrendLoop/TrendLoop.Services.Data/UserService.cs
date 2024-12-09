using Microsoft.EntityFrameworkCore;
using TrendLoop.Data.Models;
using TrendLoop.Data.Repository.Interfaces;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels.Admin;

namespace TrendLoop.Services.Data
{
    public class UserService : BaseService, IUserService
    {
        private readonly IRepository<ApplicationUser, Guid> UserRepository;
        
        public UserService(IRepository<ApplicationUser, Guid> UserRepository)
        {
            this.UserRepository = UserRepository;
        }

        public async Task<IEnumerable<UserInfoViewModel>> GetAllUsersAsync()
        {
            return await UserRepository
                 .GetAllAttached()
                 .Select(u => new UserInfoViewModel
                 {
                     Id = u.Id.ToString(),
                     Username = u.UserName,
                     SellerRating = u.SellerRating
                 }).ToListAsync();
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
