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
                    Id = p.Id.ToString(),
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
                    AttributeTypesWithValues = p.ProductAttributeValues.Select(pav => new AttributeTypeAttributeValueInfoViewModel
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

        public async Task<EditProductViewModel?> GetProductToEditAsync(Guid id)
        {
           var test = await productRepository
                .GetAllAttached()
                .Where(p => p.IsDeleted == false && p.Id == id)
                .Select(p => new EditProductViewModel
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    BrandId = p.BrandId,
                    CategoryId = p.CategoryId,
                    SubcategoryId = p.SubcategoryId,
                    CurrentAttributeTypesWithValues = p.ProductAttributeValues.Select(pav => new AttributeTypeAttributeValueInfoViewModel
                    {
                        AttributeTypeId = pav.AttributeValue.AttributeTypeId,
                        AttributeTypeName = pav.AttributeValue.AttributeType.Name,
                        AttributeValueId = pav.AttributeValueId,
                        Value = pav.AttributeValue.Value
                    })
                }).FirstOrDefaultAsync();

            return test;
        }

        public async Task<bool> EditProductAsync(Guid productId, EditProductViewModel model)
        {
            Product? productToEdit = await productRepository
                .GetAllAttached()
                .Include(p => p.ProductAttributeValues)
                .ThenInclude(pav => pav.AttributeValue)
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            if (productToEdit == null)
            {
                return false;
            }

            productToEdit.Name = model.Name;
            productToEdit.Description = model.Description;
            productToEdit.Price = model.Price;
            productToEdit.ImageUrl = model.ImageUrl;
            productToEdit.BrandId = model.BrandId;
            productToEdit.CategoryId = model.CategoryId;
            productToEdit.SubcategoryId = model.SubcategoryId;

            // Create a list to store the new values
            List<ProductAttributeValue> newProductAttributeValues = new List<ProductAttributeValue>();

            foreach (var attTypeIdAttValueId in model.NewAttributeTypeIdAttributeValueIdPairs)
            {
                // check if mapping already exists
                ProductAttributeValue? productAttributeValueMapping = productToEdit.ProductAttributeValues.FirstOrDefault(pav => pav.AttributeValueId == attTypeIdAttValueId.Value);

                if (productAttributeValueMapping == null)
                {
                    // Create a new mapping
                    productAttributeValueMapping = new ProductAttributeValue()
                    {
                        ProductId = productToEdit.Id,
                        AttributeValueId = attTypeIdAttValueId.Value
                    };
                }

                newProductAttributeValues.Add(productAttributeValueMapping);
            }

            productToEdit.ProductAttributeValues = newProductAttributeValues;

            // Update product
            try
            {
                await this.productRepository.UpdateAsync(productToEdit);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<DeleteProductViewModel?> GetProductForDeleteByIdAsync(Guid id)
        {
            DeleteProductViewModel? productToDelete = await productRepository
                .GetAllAttached()
                .Where(p => p.IsDeleted == false)
                .Select(p => new DeleteProductViewModel
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    AddedOn = p.AddedOn.ToString(AddedOnDateFormat),
                    SellerId = p.SellerId.ToString(),
                    SellerName = p.Seller.UserName.ToString()
                })
                .FirstOrDefaultAsync(p => p.Id.ToLower() == id.ToString().ToLower());

            return productToDelete;
        }

        public async Task<bool> SoftDeleteProductAsync(Guid id)
        {
            Product? productToDelete = await this.productRepository
                .FirstOrDefaultAsync(p => p.Id.ToString().ToLower() == id.ToString().ToLower());
            if (productToDelete == null)
            {
                return false;
            }

            productToDelete.IsDeleted = true;
            // Since using soft delete technique only change flag for deletion and update
            return await this.productRepository.UpdateAsync(productToDelete);
        }

        public async Task<IEnumerable<UserProductViewModel>> GetBoughtProductsByUserAsync(Guid userId)
        {
            var boughtProducts = await productRepository
                .GetAllAttached()
                .Where(p => p.IsDeleted == false && p.BuyerId == userId)
                .Select(p => new UserProductViewModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    BrandName = p.Brand.Name,
                    CategoryName = p.Category.Name,
                    SubcategoryName = p.Subcategory.Name,
                }).ToListAsync();

            return boughtProducts;
        }

        public async Task<IEnumerable<UserProductViewModel>> GetSelledProductsByUserAsync(Guid userId)
        {
            var selledProducts = await productRepository
                .GetAllAttached()
                .Where(p => p.IsDeleted == false && p.SellerId == userId)
                .Select(p => new UserProductViewModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    BrandName = p.Brand.Name,
                    CategoryName = p.Category.Name,
                    SubcategoryName = p.Subcategory.Name,
                }).ToListAsync();

            return selledProducts;
        }
    }
}