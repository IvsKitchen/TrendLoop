using TrendLoop.Web.ViewModels;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface IAttributeTypeService
    {
        public Task<IEnumerable<AttributeTypeInfoViewModel>> GetAttributeTypesBySubcategoryIdAsync(int subcategoryId);
    }
}
