using MockQueryable;
using Moq;
using System.Collections.Generic;
using System.Linq.Expressions;
using TrendLoop.Data.Models;
using TrendLoop.Data.Repository.Interfaces;
using TrendLoop.Services.Data;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels.Product;
using TrendLoop.Web.ViewModels.User;
using static TrendLoop.Common.EntityValidationConstants.Product;
namespace TrendLoop.Services.Tests
{
    public class ProductServiceTests
    {
        // Brands
        private static IList<Brand> brands = new List<Brand>()
        {
            new Brand { Id = 1, Name = "Caro Mio" },
            new Brand { Id = 2, Name = "Celeste di Oro" },
        };

        IEnumerable<BrandInfoViewModel> allBrandsInfoViewModels;

        // Categories
        private static IList<Category> categories = new List<Category>()
        {
            new Category { Id = 1, Name = "Clothing" },
            new Category { Id = 2, Name = "Shoes" },
        };

        private static IEnumerable<SubcategoryInfoViewModel> allSubcategoriesForFirstCategory;

        // Subcategories
        private static IList<Subcategory> subcategories = new List<Subcategory>()
        {
            // Clothing subcategories
            new Subcategory { Id = 1, Name = "Dresses", CategoryId = 1 },
            new Subcategory { Id = 2, Name = "Jeans", CategoryId = 1 },
            
            // Shoes subcategories
            new Subcategory { Id = 3, Name = "Boots", CategoryId = 2 },
            new Subcategory { Id = 4, Name = "Sandals", CategoryId = 2 },
        };

        private IEnumerable<CategoryInfoViewModel> allCategoriesInfoViewModels;

        // Users
        private static IList<ApplicationUser> users = new List<ApplicationUser>()
        {
            new ApplicationUser { Email = "olivia.davis@techmail.com", UserName = "olivia.davis@techmail.com", SellerRating = 3.5, AvatarUrl = "testURL"},
            new ApplicationUser { Email = "mia.clark@freshmail.net", UserName = "mia.clark@freshmail.net", SellerRating = 4.5, AvatarUrl = "testURL2"}
        };

        // Products
        private IList<Product> productsData;

        // Product repository mock
        private Mock<IRepository<Product, Guid>> productRepository;

        // Single Product
        private Product singleProduct;

        // Values for new Product
        private string newProductName;
        private string newProductDescription;
        private decimal newProductPrice;
        private string newProductImageUrl;

        // Values for edited Product
        private string editedProductName;
        private string editedProductDescription;
        private decimal editedProductPrice;

        private Guid nonExistingGuid;

        [SetUp]
        public void Setup()
        {
            productRepository = new Mock<IRepository<Product, Guid>>();
            
            newProductName = "New test product name";
            newProductDescription = "New test product description";
            newProductPrice = 100.00M;
            newProductImageUrl = "https://www.testimage.com";

            editedProductName = "Edited Product name";
            editedProductDescription = "Edited Product description";
            editedProductPrice = 1000.00M;

            nonExistingGuid = Guid.Parse("CEADDBFE-4CD2-46CB-8A7F-2B0F02CFEDE6");

            productsData = new List<Product>()
                    {
                        new Product()
                        {
                            Id = Guid.Parse("280AF1F8-0869-41F9-A929-B7C46253FFDD"),
                            Name = "Test Dress",
                            Description = "Classic and durable, with high-quality material, perfect for everyday wear and comfort.",
                            AddedOn = DateTime.Now,
                            BrandId = brands[0].Id,
                            Brand = brands[0],
                            CategoryId = categories[0].Id,
                            Category = categories[0],
                            SubcategoryId = subcategories[0].Id,
                            Subcategory = subcategories[0],
                            ImageUrl = "https://images.unsplash.com/photo-1534875756527-5e8e4392005f?q=80&w=1932&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            Price = 100,
                            SellerId = users[0].Id,
                            Seller = users[0]
                        },
                        new Product()
                        {
                            Id = Guid.Parse("280AF1F8-0869-41F9-A929-B7C46253FFDD"),
                            Name = "Test Boots",
                            Description = "Classic and durable, with high-quality material, perfect for everyday wear and comfort.",
                            AddedOn = DateTime.Now,
                            Brand = brands[1],
                            BrandId = brands[1].Id,
                            CategoryId = categories[1].Id,
                            Category = categories[1],
                            SubcategoryId = subcategories[3].Id,
                            Subcategory = subcategories[3],
                            ImageUrl = "https://images.unsplash.com/photo-1494955464529-790512c65305?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            Price = 200,
                            SellerId = users[1].Id,
                            Seller =  users[1]
                        }
                    };

            singleProduct = new Product()
            {
                Id = Guid.Parse("280AF1F8-0869-41F9-A929-B7C46253FFDD"),
                Name = "Test Dress",
                Description = "Classic and durable, with high-quality material, perfect for everyday wear and comfort.",
                AddedOn = DateTime.Now,
                BrandId = brands[0].Id,
                Brand = brands[0],
                CategoryId = categories[0].Id,
                Category = categories[0],
                SubcategoryId = subcategories[0].Id,
                Subcategory = subcategories[0],
                ImageUrl = "https://images.unsplash.com/photo-1534875756527-5e8e4392005f?q=80&w=1932&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                Price = 100,
                SellerId = users[0].Id,
                Seller = users[0]
            };

            allBrandsInfoViewModels = new HashSet<BrandInfoViewModel>()
            {
                new BrandInfoViewModel { Id = brands[0].Id, Name = brands[0].Name },
                new BrandInfoViewModel { Id = brands[1].Id, Name = brands[1].Name }
            };

            allSubcategoriesForFirstCategory = new HashSet<SubcategoryInfoViewModel>()
            {
                new SubcategoryInfoViewModel { Id = subcategories[0].Id, Name = subcategories[0].Name, CategoryId = categories[0].Id },
                new SubcategoryInfoViewModel { Id = subcategories[1].Id, Name = subcategories[1].Name, CategoryId = categories[0].Id }
            };

            allCategoriesInfoViewModels = new HashSet<CategoryInfoViewModel>()
            {
                new CategoryInfoViewModel { Id = categories[0].Id, Name = categories[0].Name, Subcategories = allSubcategoriesForFirstCategory }
            };

        }

        [Test]
        public async Task TestGetAllProductsNoFilterPositive()
        {
            IQueryable<Product> productMockQueryable = productsData.BuildMock();
            this.productRepository
                .Setup(p => p.GetAllAttached())
                .Returns(productMockQueryable);

            IProductService productService = new ProductService(productRepository.Object);

            IEnumerable<ProductViewModel> allProductsActual = await productService
                .GetAllProductsAsync(new ProductsViewModel());

            Assert.IsNotNull(allProductsActual);
            Assert.That(allProductsActual.Count(), Is.EqualTo(productsData.Count()));

            allProductsActual = allProductsActual
                .OrderBy(m => m.Id)
                .ToList();

            int index = 0;

            foreach (ProductViewModel returnedProduct in allProductsActual)
            {
                Assert.IsNotNull(returnedProduct);
                Assert.AreEqual(productsData.OrderBy(p => p.Id).ToList()[index++].Id.ToString(), returnedProduct.Id);
            }
        }

        [Test]
        [TestCase("dress")]
        [TestCase("DR")]
        public async Task TestGetAllProductsSearchQueryPositive(string searchQuery)
        {
            int expectedProductsCount = 1;
            string expectedProductId = "280AF1F8-0869-41F9-A929-B7C46253FFDD";

            IQueryable<Product> productMockQueryable = productsData.BuildMock();
            this.productRepository
                .Setup(p => p.GetAllAttached())
                .Returns(productMockQueryable);

            IProductService productService = new ProductService(productRepository.Object);
            IEnumerable<ProductViewModel> allProductsActual = await productService
                .GetAllProductsAsync(new ProductsViewModel()
                {
                    SearchQuery = searchQuery,
                });

            Assert.IsNotNull(allProductsActual);
            Assert.AreEqual(expectedProductsCount, allProductsActual.Count());
            Assert.AreEqual(expectedProductId.ToLower(), allProductsActual.First().Id.ToLower());
        }

        [Test]
        [TestCase("Caro Mio")]
        public async Task TestGetAllProductsBrandFilterPositive(string brandFilter)
        {
            int expectedProductsCount = 1;
            string expectedProductId = "280AF1F8-0869-41F9-A929-B7C46253FFDD";

            IQueryable<Product> productMockQueryable = productsData.BuildMock();
            this.productRepository
                .Setup(p => p.GetAllAttached())
                .Returns(productMockQueryable);

            IProductService productService = new ProductService(productRepository.Object);
            IEnumerable<ProductViewModel> allProductsActual = await productService
                .GetAllProductsAsync(new ProductsViewModel()
                {
                    BrandFilter = brandFilter,
                });

            Assert.IsNotNull(allProductsActual);
            Assert.AreEqual(expectedProductsCount, allProductsActual.Count());
            Assert.AreEqual(expectedProductId.ToLower(), allProductsActual.First().Id.ToLower());
        }

        [Test]
        [TestCase("Clothing")]
        public async Task TestGetAllProductsCategoryFilterPositive(string categoryFilter)
        {
            int expectedProductsCount = 1;
            string expectedProductId = "280AF1F8-0869-41F9-A929-B7C46253FFDD";

            IQueryable<Product> productMockQueryable = productsData.BuildMock();
            this.productRepository
                .Setup(p => p.GetAllAttached())
                .Returns(productMockQueryable);

            IProductService productService = new ProductService(productRepository.Object);
            IEnumerable<ProductViewModel> allProductsActual = await productService
                .GetAllProductsAsync(new ProductsViewModel()
                {
                    CategoryFilter = categoryFilter,
                });

            Assert.IsNotNull(allProductsActual);
            Assert.AreEqual(expectedProductsCount, allProductsActual.Count());
            Assert.AreEqual(expectedProductId.ToLower(), allProductsActual.First().Id.ToLower());
        }

        [Test]
        [TestCase("Dresses")]
        public async Task TestGetAllProductsSubcategoryFilterPositive(string subcategoryFilter)
        {
            int expectedProductsCount = 1;
            string expectedProductId = "280AF1F8-0869-41F9-A929-B7C46253FFDD";

            IQueryable<Product> productMockQueryable = productsData.BuildMock();
            this.productRepository
                .Setup(p => p.GetAllAttached())
                .Returns(productMockQueryable);

            IProductService productService = new ProductService(productRepository.Object);
            IEnumerable<ProductViewModel> allProductsActual = await productService
                .GetAllProductsAsync(new ProductsViewModel()
                {
                    SubcategoryFilter = subcategoryFilter,
                });

            Assert.IsNotNull(allProductsActual);
            Assert.AreEqual(expectedProductsCount, allProductsActual.Count());
            Assert.AreEqual(expectedProductId.ToLower(), allProductsActual.First().Id.ToLower());
        }

        [Test]
        public async Task TestGetAllProductsNullFilterNegative()
        {
            IQueryable<Product> productMockQueryable = productsData.BuildMock();
            this.productRepository
                .Setup(p => p.GetAllAttached())
                .Returns(productMockQueryable);

            IProductService productService = new ProductService(productRepository.Object);
            Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                IEnumerable<ProductViewModel> allProductsActual = await productService
                    .GetAllProductsAsync(null);
            });
        }

        [Test]
        public async Task TestGetAllProductsCount()
        {
            int expectedCount = productsData.Count;
            IQueryable<Product> productMockQueryable = productsData.BuildMock();
            this.productRepository
                .Setup(p => p.GetAllAttached())
                .Returns(productMockQueryable);

            IProductService productService = new ProductService(productRepository.Object);
            int actualCount = await productService.GetAllProductsCountAsync(new ProductsViewModel());
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public async Task TestAddProductAsync()
        {
            AddProductViewModel model = new AddProductViewModel()
            {
                Name = newProductName,
                Description = newProductDescription,
                Price = newProductPrice,
                AddedOn = DateTime.Now,
                BrandId = brands[0].Id,
                CategoryId = categories[0].Id,
                SubcategoryId = subcategories[0].Id,
                ImageUrl = newProductImageUrl,
                Brands = allBrandsInfoViewModels,
                Categories = allCategoriesInfoViewModels,
            };

            IProductService productService = new ProductService(productRepository.Object);
            bool isAdded = await productService.AddProductAsync(users[0].Id, model);
            Assert.IsTrue(isAdded);
        }

        [Test]
        public async Task TestGetProductToEditAsync()
        {
            IQueryable<Product> productMockQueryable = productsData.BuildMock();
            this.productRepository
                .Setup(p => p.GetAllAttached())
                .Returns(productMockQueryable);

            IProductService productService = new ProductService(productRepository.Object);
            Product testProduct = productsData[0];

            EditProductViewModel? model = await productService.GetProductToEditAsync(testProduct.Id);
            Assert.IsNotNull(model);

            Assert.That(model.Id, Is.EqualTo(testProduct.Id.ToString()), "Product ID mismatch.");
            Assert.That(model.Name, Is.EqualTo(testProduct.Name), "Product Name mismatch.");
            Assert.That(model.Description, Is.EqualTo(testProduct.Description), "Product Description mismatch.");
            Assert.That(model.Price, Is.EqualTo(testProduct.Price), "Product Price mismatch.");
            Assert.That(model.BrandId, Is.EqualTo(testProduct.Brand.Id), "Product Brand mismatch.");
            Assert.That(model.CategoryId, Is.EqualTo(testProduct.Category.Id), "Product Category mismatch.");
            Assert.That(model.SubcategoryId, Is.EqualTo(testProduct.Subcategory.Id), "Product Subcategory mismatch.");
        }

        [Test]
        public async Task TestEditProduct()
        {
            this.productRepository
                .Setup(repo => repo.GetById(productsData[0].Id))
                .Returns(singleProduct);

            // Create EditProductViewModel for the first product with GUID productsData[0].Id
            EditProductViewModel editedProductModel = new EditProductViewModel
            {
                // Edit name
                Name = editedProductName,
                // Edit description
                Description = editedProductDescription,
                // Edit price
                Price = editedProductPrice,
                // Leave other values the same
                ImageUrl = productsData[0].ImageUrl,
                BrandId = productsData[0].BrandId,
                CategoryId = productsData[0].CategoryId,
                SubcategoryId = productsData[0].SubcategoryId,
                Brands = allBrandsInfoViewModels,
                Categories = allCategoriesInfoViewModels
            };

            IQueryable<Product> productMockQueryable = productsData.BuildMock();
            this.productRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(productMockQueryable);

            IProductService productService = new ProductService(productRepository.Object);
            bool isEdited = await productService.EditProductAsync(productsData[0].Id, editedProductModel);
            Assert.IsTrue(isEdited);
            // Verify Product name is edited
            Assert.That(productsData[0].Name, Is.EqualTo(editedProductName));
            // Verify Product description is edited
            Assert.That(productsData[0].Description, Is.EqualTo(editedProductDescription));
            // Verify Product price is edited
            Assert.That(productsData[0].Price, Is.EqualTo(editedProductPrice));


            // Test with non-existing product GUID
            bool isEditedNonExistingGuid = await productService.EditProductAsync(nonExistingGuid, editedProductModel);
            Assert.IsFalse(isEditedNonExistingGuid);

            // Test repository throws an exception
            productRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ThrowsAsync(new Exception("Repository error"));
            bool isEditedRepoError = await productService.EditProductAsync(productsData[0].Id, editedProductModel);
            Assert.IsFalse(isEditedRepoError);
        }

        [Test]
        public async Task TestGetProductToBuyAsync()
        {
            IQueryable<Product> productMockQueryable = productsData.BuildMock();
            this.productRepository
                .Setup(p => p.GetAllAttached())
                .Returns(productMockQueryable);

            IProductService productService = new ProductService(productRepository.Object);
            Product testProduct = productsData[0];

            BuyProductViewModel model = await productService.GetProductToBuyAsync(testProduct.Id);
            Assert.IsNotNull(model);

            Assert.That(model.Id, Is.EqualTo(testProduct.Id.ToString()), "Product ID mismatch.");
            Assert.That(model.Name, Is.EqualTo(testProduct.Name), "Product Name mismatch.");
            Assert.That(model.Description, Is.EqualTo(testProduct.Description), "Product Description mismatch.");
            Assert.That(model.Price, Is.EqualTo(testProduct.Price.ToString("F2")), "Product Price mismatch.");
            Assert.That(model.BrandName, Is.EqualTo(testProduct.Brand.Name), "Product Brand name mismatch.");
            Assert.That(model.CategoryName, Is.EqualTo(testProduct.Category.Name), "Product Category name mismatch.");
            Assert.That(model.SubcategoryName, Is.EqualTo(testProduct.Subcategory.Name), "Product Subcategory name mismatch.");
        }

        [Test]
        public async Task TestBuyProductAsync()
        {
            IQueryable<Product> productMockQueryable = productsData.BuildMock();
            this.productRepository
                .Setup(p => p.GetByIdAsync(productsData[0].Id)).ReturnsAsync(productsData[0]);

            IProductService productService = new ProductService(productRepository.Object);
            Guid productToBuy = productsData[0].Id;
            Guid productBuyer = users[0].Id;

            bool isBought = await productService.BuyProductAsync(productToBuy, productBuyer);
            Assert.IsTrue(isBought);

            // Test repository throws an exception
            productRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ThrowsAsync(new Exception("Repository error"));
            bool isBoughtRepoError = await productService.BuyProductAsync(productToBuy, productBuyer);
            Assert.IsFalse(isBoughtRepoError);

            // Test non existing Product
            bool isBoughtNonExistingProduct = await productService.BuyProductAsync(nonExistingGuid, productBuyer);
            Assert.IsFalse(isBoughtNonExistingProduct);

            // Test non existing Buyer
            bool isBoughtNonExistingBuyer = await productService.BuyProductAsync(productToBuy, nonExistingGuid);
            Assert.IsFalse(isBoughtNonExistingBuyer);
        }

        [Test]
        public async Task TestGetProductForDeleteByIdAsync()
        {
            IQueryable<Product> productMockQueryable = productsData.BuildMock();
            this.productRepository
                .Setup(p => p.GetAllAttached())
                .Returns(productMockQueryable);

            IProductService productService = new ProductService(productRepository.Object);
            Product testProduct = productsData[0];

            DeleteProductViewModel? model = await productService.GetProductForDeleteByIdAsync(testProduct.Id);
            Assert.IsNotNull(model);

            Assert.That(model.Id, Is.EqualTo(testProduct.Id.ToString()), "Product ID mismatch.");
            Assert.That(model.Name, Is.EqualTo(testProduct.Name), "Product Name mismatch.");
            Assert.That(model.AddedOn, Is.EqualTo(testProduct.AddedOn.ToString(AddedOnDateFormat)), "Product AddedOn mismatch.");
            Assert.That(model.SellerName, Is.EqualTo(testProduct.Seller.UserName), "Product Seller name mismatch.");
            Assert.That(model.SellerId.ToLower(), Is.EqualTo(testProduct.SellerId.ToString().ToLower()), "Product Seller ID mismatch.");
        }

        [Test]
        public async Task TestSoftDeleteProductAsync()
        {
            Product productToDelete = productsData[0];

            // Setup mock to return the product to delete
            productRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                             .ReturnsAsync(productToDelete);

            // Setup mock for UpdateAsync to return true (simulating a successful update)
            productRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                             .ReturnsAsync(true);

            IProductService productService = new ProductService(productRepository.Object);

            bool isDeleted = await productService.SoftDeleteProductAsync(productToDelete.Id);

            // Assert: Ensure the method returns true when product is soft deleted
            Assert.IsTrue(isDeleted);

            // Verify the IsDeleted flag is set to true
            Assert.IsTrue(productToDelete.IsDeleted);
        }

        [Test]
        public async Task TestSoftDeleteProductAsyncProductNotFound()
        {
            // Setup mock to return null when no product is found
            productRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                             .ReturnsAsync((Product)null);

            IProductService productService = new ProductService(productRepository.Object);

            bool isDeletedNoProductFound = await productService.SoftDeleteProductAsync(productsData[0].Id);

            // Ensure the method returns false when product is not found
            Assert.IsFalse(isDeletedNoProductFound);
        }

        [Test]
        public async Task TestGetBoughtProductsByUserAsync()
        {
            Guid buyerId = users[0].Id;
            Product boughtProduct = productsData[0];
            boughtProduct.BuyerId = buyerId;

            IQueryable<Product> productMockQueryable = productsData.BuildMock();
            
            this.productRepository
                .Setup(p => p.GetAllAttached())
                .Returns(productMockQueryable);

            IProductService productService = new ProductService(productRepository.Object);

            IEnumerable<UserProductViewModel> boughtProducts = await productService.GetBoughtProductsByUserAsync(buyerId);
            // Ensure there are bought Products
            Assert.IsTrue(boughtProducts.Any());
            Assert.That(boughtProducts.Count(), Is.EqualTo(1));
        }
    }
}

