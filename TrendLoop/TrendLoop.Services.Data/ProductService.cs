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
                    AddedOn = p.AddedOn.ToString(AddedOnDateFormat),
                    BrandName = p.Brand.Name,
                    CategoryName = p.Category.Name,
                    SubcategoryName = p.Subcategory.Name,
                    SellerName = p.Seller.UserName,
                    SellerRating = p.Seller.SellerRating,
                    SellerAvatarUrl = p.Seller.AvatarUrl
                })
                .ToListAsync();
        }

        public async Task<ProductDetailsViewModel> GetProductDetailsAsync(Guid productId)
        {
            return await productRepository
                .GetAllAttached()
                .Where(p => !p.IsDeleted && p.Id == productId)
                .Select(p => new ProductDetailsViewModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price.ToString("F2"),
                    //Size = p.ProductAttributeValues.FirstOrDefault(pav => pav.AttributeValue.AttributeType.Name.ToLower().Contains("size")).AttributeValue.Value,
                    ImageUrl = p.ImageUrl,
                    AddedOn = p.AddedOn.ToString(AddedOnDateFormat),
                    BrandName = p.Brand.Name,
                    CategoryName = p.Category.Name,
                    SubcategoryName = p.Subcategory.Name,
                    SellerName = p.Seller.UserName,
                    SellerRating = p.Seller.SellerRating,
                    SellerAvatarUrl = p.Seller.AvatarUrl,
                    AttributeTypesWithValues = p.ProductAttributeValues.Select(pav => new AttributeTypeValueInfoViewModel 
                    { 
                        AttributeTypeId = pav.AttributeValue.AttributeTypeId,
                        AttributeTypeName = pav.AttributeValue.AttributeType.Name,
                        AttributeValueId = pav.AttributeValueId,
                        Value = pav.AttributeValue.Value
                    })
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> AddProductAsync(Guid sellerId, AddProductViewModel model)
        {
            Product product = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                BrandId = model.BrandId,
                CategoryId = model.CategoryId,
                SubcategoryId = model.SubcategoryId,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                AddedOn = DateTime.Now,
                SellerId = sellerId,
            };

            List<ProductAttributeValue> productAttributeValues = new List<ProductAttributeValue>();

            foreach (var attTypeIdAttValueId in model.AttributeTypeIdAttributeValueIdPair)
            {
                ProductAttributeValue productAttribute = new ProductAttributeValue()
                {
                    ProductId = product.Id,
                    AttributeValueId = attTypeIdAttValueId.Value
                };

                productAttributeValues.Add(productAttribute);
            }

            product.ProductAttributeValues = productAttributeValues;

            await this.productRepository.AddAsync(product);

            return true;
        }
    }
}