using TrendLoop.Web.ViewModels;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface IAttributeValueService
    {
        public Task<IEnumerable<AttributeValueInfoViewModel>> GetAttributeValuesByAttributeTypeIdAsync(int categoryId);
    }
}
