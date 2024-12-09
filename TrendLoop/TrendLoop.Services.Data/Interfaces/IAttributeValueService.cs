using TrendLoop.Web.ViewModels.Product;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface IAttributeValueService
    {
        public Task<IEnumerable<AttributeValueInfoViewModel>> GetAttributeValuesByAttributeTypeIdAsync(int categoryId);
    }
}
