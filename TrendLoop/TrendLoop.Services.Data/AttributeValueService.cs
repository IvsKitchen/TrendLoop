using Microsoft.EntityFrameworkCore;
using System.Linq;
using TrendLoop.Data.Models;
using TrendLoop.Data.Repository.Interfaces;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels.Product;

namespace TrendLoop.Services.Data
{
    public class AttributeValueService : BaseService, IAttributeValueService
    {
        private readonly IRepository<AttributeValue, int> attributeValueRepository;

        public AttributeValueService(IRepository<AttributeValue, int> attributeValueRepository)
        {
            this.attributeValueRepository = attributeValueRepository;
        }

        public async Task<IEnumerable<AttributeValueInfoViewModel>> GetAttributeValuesByAttributeTypeIdAsync(int attributeTypeId)
        {
            return await attributeValueRepository
                .GetAllAttached()
                .Where(av => av.AttributeTypeId == attributeTypeId)
                .Select(av => new AttributeValueInfoViewModel
                {
                    Id = av.Id,
                    Value = av.Value,
                })
                .ToListAsync();
        }
    }
}
