using TrendLoop.Web.ViewModels.Admin;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsUserProductSeller(Guid userId, Guid productId);

        Task<IEnumerable<UserInfoViewModel>> GetAllUsersAsync();
    }
}
