namespace TrendLoop.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsUserProductSeller(Guid userId, Guid productId);
    }
}
