using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrendLoop.Web.ViewModels;

namespace TrendLoop.Services.Data.Interfaces
{
    public interface IAttributeTypeService
    {
        public Task<IEnumerable<AttributeTypeInfoViewModel>> GetAttributeTypesBySubcategoryIdAsync(int subcategoryId);
    }
}
