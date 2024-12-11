using MockQueryable;
using Moq;
using TrendLoop.Data.Models;
using TrendLoop.Services.Data;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels.Product;

namespace TrendLoop.Services.Tests
{
    public class PurchaseServiceTests
    {
        // ProductService mock
        private Mock<IProductService> productService;

        // Product to buy
        private Product productToBuy;

        // 
        private Product productToBuyNegative;

        // Product seller
        private ApplicationUser seller;

        // Product buyer
        private ApplicationUser buyer;

        [SetUp]
        public void Setup()
        {
            productService = new Mock<IProductService>();

            seller = new ApplicationUser()
            {
                Id = Guid.Parse("02C82C25-C238-4EFC-97CB-83D511F1D360"),
                Email = "john.doe@mailbox.com",
                UserName = "John123!",
                SellerRating = 4
            };

            buyer = new ApplicationUser()
            {
                Id = Guid.Parse("0AEE9B0D-6D59-42F9-89B8-B8297A1FCE7B"),
                Email = "jane.smith@outlook.net",
                UserName = "Jane123!",
                SellerRating = 4
            };

            productToBuy = new Product()
            {
                Id = Guid.Parse("280AF1F8-0869-41F9-A929-B7C46253FFDD"),
                Name = "Test",
                Description = "Test",
                AddedOn = DateTime.Now,
                BrandId = 1,
                CategoryId = 1,
                SubcategoryId = 1,
                Price = 200,
                SellerId = seller.Id,
                Seller = seller
            };

            productToBuyNegative = new Product()
            {
                Id = Guid.Parse("236AF1F8-0869-41F9-A929-B7C46253FFDD"),
                Name = "Test",
                Description = "Test",
                AddedOn = DateTime.Now,
                BrandId = 1,
                CategoryId = 1,
                SubcategoryId = 1,
                Price = 200,
                SellerId = seller.Id,
                Seller = seller
            };
        }

        [Test]
        public async Task TestGetProductToBuyAsync()
        {
            BuyProductViewModel buyProductmodel = new BuyProductViewModel()
            {
                Id = productToBuy.Id.ToString(),
                Name = productToBuy.Name,
                Description = productToBuy.Description,
                Price = productToBuy.Price.ToString("F2"),
            };

            productService.Setup(ps => ps.GetProductToBuyAsync(productToBuy.Id)).ReturnsAsync(buyProductmodel);

            IPurchaseService purchaseService = new PurchaseService(productService.Object);

            BuyProductViewModel actualBuyProductModel = await purchaseService.GetProductToBuyAsync(productToBuy.Id);
            Assert.IsNotNull(actualBuyProductModel);
            Assert.AreEqual(buyProductmodel.Id, actualBuyProductModel.Id);
            Assert.AreEqual(buyProductmodel.Name, actualBuyProductModel.Name);
            Assert.AreEqual(buyProductmodel.Description, actualBuyProductModel.Description);
            Assert.AreEqual(buyProductmodel.Price, actualBuyProductModel.Price);
        }

        [Test]
        public async Task TestExecuteBuyProductAsync()
        {
            productService.Setup(ps => ps.BuyProductAsync(productToBuy.Id, buyer.Id)).ReturnsAsync(true);

            IPurchaseService purchaseService = new PurchaseService(productService.Object);

            bool isBought = await purchaseService.ExecuteBuyProductAsync(productToBuy.Id, buyer.Id);
            Assert.IsTrue(isBought);

            // Test negative case
            productService.Setup(ps => ps.BuyProductAsync(productToBuyNegative.Id, buyer.Id)).ReturnsAsync(false);
            bool isBoughtNegative = await purchaseService.ExecuteBuyProductAsync(productToBuyNegative.Id, buyer.Id);
            Assert.IsFalse(isBoughtNegative);
        }

    }
}

