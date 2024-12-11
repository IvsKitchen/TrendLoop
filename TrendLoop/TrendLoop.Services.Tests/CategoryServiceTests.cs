using MockQueryable;
using Moq;
using System.Collections.Generic;
using System.Linq.Expressions;
using TrendLoop.Data.Models;
using TrendLoop.Data.Repository.Interfaces;
using TrendLoop.Services.Data;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels.Product;

namespace TrendLoop.Services.Tests
{
    public class CategoryServiceTests
    {
        // Categories
        private static IList<Category> categoriesData = new List<Category>()
        {
            new Category { Id = 1, Name = "Clothing" },
            new Category { Id = 2, Name = "Shoes" },
        };

        // Category repository mock
        private Mock<IRepository<Category, int>> categoryRepository;

        [SetUp]
        public void Setup()
        {
            categoryRepository = new Mock<IRepository<Category, int>>();
        }

        [Test]
        public async Task TestGetAllCategoriesAsync()
        {
            IQueryable<Category> categoriesMockQueryable = categoriesData.BuildMock();
            this.categoryRepository
                .Setup(p => p.GetAllAttached())
                .Returns(categoriesMockQueryable);

            ICategoryService categoryService = new CategoryService(categoryRepository.Object);

            IEnumerable<CategoryInfoViewModel> categoryModels = await categoryService.GetAllCategoriesAsync();
            Assert.IsTrue(categoryModels.Any());
            Assert.That(categoryModels.Count(), Is.EqualTo(categoriesData.Count));
        }


        [Test]
        public async Task TestGetAllCategoriesNamesAsync()
        {
            IQueryable<Category> categoryMockQueryable = categoriesData.BuildMock();
            this.categoryRepository
                .Setup(p => p.GetAllAttached())
                .Returns(categoryMockQueryable);

            ICategoryService categoryService = new CategoryService(categoryRepository.Object);

            IEnumerable<string> categoriesNames = await categoryService.GetAllCategoriesNamesAsync();
            Assert.IsTrue(categoriesNames.Any());
            Assert.That(categoriesNames.Count(), Is.EqualTo(categoriesData.Count));
        }
    }
}

