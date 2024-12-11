using MockQueryable;
using Moq;
using TrendLoop.Data.Models;
using TrendLoop.Data.Repository.Interfaces;
using TrendLoop.Services.Data;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels.Areas.Admin.User;

namespace TrendLoop.Services.Tests
{
    public class UserServiceTests
    {
        // Users
        private static IList<ApplicationUser> usersData;

        // User repository mock
        private Mock<IRepository<ApplicationUser, Guid>> userRepository;

        // Product
        IList<Product> products;

        [SetUp]
        public void Setup()
        {
            userRepository = new Mock<IRepository<ApplicationUser, Guid>>();

            usersData = new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    Id = Guid.Parse("02C82C25-C238-4EFC-97CB-83D511F1D360"),
                    Email = "john.doe@mailbox.com",
                    UserName = "John123!",
                    SellerRating = 4
                },
                new ApplicationUser
                {
                    Id = Guid.Parse("0AEE9B0D-6D59-42F9-89B8-B8297A1FCE7B"),
                    Email = "jane.smith@outlook.net",
                    UserName = "Jane456!",
                    SellerRating = 4
                }
            };

            products = new List<Product>()
            {
               new Product()
               {
                   Id = Guid.Parse("280AF1F8-0869-41F9-A929-B7C46253FFDD"),
                   Name = "Test Dress",
                   Description = "Classic and durable, with high-quality material, perfect for everyday wear and comfort.",
                   AddedOn = DateTime.Now,
                   BrandId = 1,
                   CategoryId = 1,
                   SubcategoryId = 1,
                   ImageUrl = "https://images.unsplash.com/photo-1534875756527-5e8e4392005f?q=80&w=1932&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                   Price = 100,
                   SellerId = usersData[0].Id,
                   Seller = usersData[0],
               },
               new Product()
               {
                   Id = Guid.Parse("360AF1F8-0869-41F9-A929-B7C46253FFDD"),
                   Name = "Test Boots",
                   Description = "Classic and durable, with high-quality material, perfect for everyday wear and comfort.",
                   AddedOn = DateTime.Now,
                   BrandId = 2,
                   CategoryId = 2,
                   SubcategoryId = 2,
                   ImageUrl = "https://images.unsplash.com/photo-1494955464529-790512c65305?q=80&w=2071&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                   Price = 200,
                   SellerId = usersData[1].Id,
                   Seller =  usersData[1]
               }
            };

            usersData[0].ProductsForSale = new HashSet<Product>() { products[0] };
        }

        [Test]
        public async Task TestGetAllUsersAsync()
        {
            IQueryable<ApplicationUser> usersMockQueryable = usersData.BuildMock();
            this.userRepository
                .Setup(u => u.GetAllAttached())
                .Returns(usersMockQueryable);

            IUserService userService = new UserService(userRepository.Object);

            IEnumerable<UserInfoViewModel> userModels = await userService.GetAllUsersAsync();
            Assert.IsTrue(userModels.Any());
            Assert.That(userModels.Count(), Is.EqualTo(usersData.Count));
        }


        [Test]
        public async Task TestIsUserProductSeller()
        {
            IQueryable<ApplicationUser> usersMockQueryable = usersData.BuildMock();
            this.userRepository
                .Setup(u => u.GetAllAttached())
                .Returns(usersMockQueryable);

            IUserService userService = new UserService(userRepository.Object);

            bool isUserProductSellerPositive = await userService.IsUserProductSeller(usersData[0].Id, products[0].Id);
            Assert.IsTrue(isUserProductSellerPositive);

            bool isUserProductSellerNegative = await userService.IsUserProductSeller(usersData[0].Id, products[1].Id);
            Assert.IsFalse(isUserProductSellerNegative);
        }
    }
}

