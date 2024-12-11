using MockQueryable;
using Moq;
using TrendLoop.Data.Models;
using TrendLoop.Data.Repository.Interfaces;
using TrendLoop.Services.Data;
using TrendLoop.Services.Data.Interfaces;
using TrendLoop.Web.ViewModels.Product;

namespace TrendLoop.Services.Tests
{
    public class SubcategoryServiceTests
    {
        private Mock<IRepository<Subcategory, int>> subcategoryRepository;
        IList<Category> categories;
        IList<Subcategory> subcategoriesData;

        [SetUp]
        public void Setup()
        {
            subcategoryRepository = new Mock<IRepository<Subcategory, int>>();

            categories = new List<Category>()
            {
                new Category { Id = 1, Name = "Clothing" },
                new Category { Id = 2, Name = "Shoes" },
            };

            subcategoriesData = new List<Subcategory>()
            {
                // Clothing subcategories
                new Subcategory { Id = 1, Name = "Dresses", CategoryId = 1 },
                new Subcategory { Id = 2, Name = "Jeans", CategoryId = 1 },
                
                // Shoes subcategories
                new Subcategory { Id = 3, Name = "Boots", CategoryId = 2 },
                new Subcategory { Id = 4, Name = "Sandals", CategoryId = 2 },
            };
    }

        [Test]
        public async Task TestGetAllSubcategoriesAsync()
        {
            IQueryable<Subcategory> subcategoriesMockQueryable = subcategoriesData.BuildMock();
            this.subcategoryRepository
                .Setup(p => p.GetAllAttached())
                .Returns(subcategoriesMockQueryable);

            ISubcategoryService subcategoryService = new SubcategoryService(subcategoryRepository.Object);

            IEnumerable<SubcategoryInfoViewModel> subcategoriesModels = await subcategoryService.GetAllSubcategoriesAsync();
            Assert.IsTrue(subcategoriesModels.Any());
            Assert.That(subcategoriesModels.Count(), Is.EqualTo(subcategoriesData.Count));
        }

        [Test]
        public async Task TestGetSubcategoriesByCategoryIdAsync()
        {
            IQueryable<Subcategory> subcategoryMockQueryable = subcategoriesData.BuildMock();
            this.subcategoryRepository
                .Setup(p => p.GetAllAttached())
                .Returns(subcategoryMockQueryable);

            ISubcategoryService subcategoryService = new SubcategoryService(subcategoryRepository.Object);

            IEnumerable<SubcategoryInfoViewModel> subcategoriesModels = await subcategoryService.GetSubcategoriesByCategoryIdAsync(categories[0].Id);
            Assert.IsTrue(subcategoriesModels.Any());
            Assert.That(subcategoriesModels.Count(), Is.EqualTo(subcategoriesData.Where(sc => sc.CategoryId == categories[0].Id).Count()));
        }

        [Test]
        public async Task TestGetAllSubcategoriesNamesAsync()
        {
            IQueryable<Subcategory> subcategoryMockQueryable = subcategoriesData.BuildMock();
            this.subcategoryRepository
                .Setup(p => p.GetAllAttached())
                .Returns(subcategoryMockQueryable);

            ISubcategoryService subcategoryService = new SubcategoryService(subcategoryRepository.Object);

            IEnumerable<string> subcategoriesNames = await subcategoryService.GetAllSubcategoriesNamesAsync();
            Assert.IsTrue(subcategoriesNames.Any());
            Assert.That(subcategoriesNames.Count(), Is.EqualTo(subcategoriesData.Count));
        }
    }
}

