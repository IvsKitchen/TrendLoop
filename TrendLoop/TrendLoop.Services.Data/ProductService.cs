using Microsoft.EntityFrameworkCore;
using TrendLoop.Data.Models;
using TrendLoop.Data.Repository.Interfaces;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels;

using static TrendLoop.Common.EntityValidationConstants.Product;

namespace TrendLoop.Services.Data
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IRepository<Product, Guid> productRepository;

        public ProductService(IRepository<Product, Guid> productRepository)
        {
            this.productRepository = productRepository;

        }

        public async Task<IEnumerable<AllProductsIndexViewModel>> GetAllProductsAsync()
        {
            return await productRepository
                .GetAllAttached()
                .Where(p => !p.IsDeleted)
                .Select(p => new AllProductsIndexViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price.ToString("F2"),
                    Size = p.ProductAttributeValues.FirstOrDefault(pav => pav.AttributeValue.AttributeType.Name.ToLower().Contains("size")).AttributeValue.Value,
                    ImageUrl = p.ImageUrl,
                    AddedOn = p.AddedOn.ToString("yyyy-MM-dd"),
                    BrandName = p.Brand.Name,
                    CategoryName = p.Category.Name,
                    SubcategoryName = p.Subcategory.Name,
                    SellerName = p.Seller.UserName,
                    SellerRating = p.Seller.SellerRating,
                    SellerAvatarUrl = p.Seller.AvatarUrl
                })
                .ToListAsync();
        }
    }
}
