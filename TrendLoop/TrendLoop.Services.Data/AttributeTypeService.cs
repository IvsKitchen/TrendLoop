using Microsoft.EntityFrameworkCore;
using TrendLoop.Data.Models;
using TrendLoop.Data.Repository.Interfaces;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels;

namespace TrendLoop.Services.Data
{
    public class AttributeTypeService : BaseService, IAttributeTypeService
    {
        private readonly IRepository<AttributeType, int> attributeTypeRepository;
        private readonly IRepository<AttributeValue, int> attributeValueRepository;

        public AttributeTypeService(IRepository<AttributeType, int> attributeTypeRepository, IRepository<AttributeValue, int> attributeValueRepository)
        {
            this.attributeTypeRepository = attributeTypeRepository;
            this.attributeValueRepository = attributeValueRepository;
        }

        public async Task<IEnumerable<AttributeTypeInfoViewModel>> GetAttributeTypesBySubcategoryIdAsync(int subcategoryId)
        {
            return await attributeTypeRepository
                .GetAllAttached()
                .Where(at => at.SubcategoryAttributeTypes.Any(sat => sat.SubcategoryId == subcategoryId))
                .Select(at => new AttributeTypeInfoViewModel
                {
                    Id = at.Id,
                    Name = at.Name,
                    AttributeValues = at.AttributeValues 
                                                    .Select(av => new AttributeValueInfoViewModel
                                                    {
                                                        Id = av.Id,
                                                        Value = av.Value,
                                                        AttributeTypeId = at.Id
                                                    })
                })
                .ToListAsync();
        }
    }
}
