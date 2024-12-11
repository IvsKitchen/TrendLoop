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
    public class BrandServiceTests
    {
        // Brands
        private static IList<Brand> brandsData = new List<Brand>()
        {
            new Brand { Id = 1, Name = "Caro Mio" },
            new Brand { Id = 2, Name = "Celeste di Oro" },
        };

        // Brand repository mock
        private Mock<IRepository<Brand, int>> brandRepository;

        [SetUp]
        public void Setup()
        {
            brandRepository = new Mock<IRepository<Brand, int>>();
        }

        [Test]
        public async Task TestGetAllBrandsAsync()
        {
            IQueryable<Brand> brandMockQueryable = brandsData.BuildMock();
            this.brandRepository
                .Setup(p => p.GetAllAttached())
                .Returns(brandMockQueryable);

            IBrandService brandService = new BrandService(brandRepository.Object);

            IEnumerable<BrandInfoViewModel> brandModels = await brandService.GetAllBrandsAsync();
            Assert.IsTrue(brandModels.Any());
            Assert.That(brandModels.Count(), Is.EqualTo(brandsData.Count));
        }


        [Test]
        public async Task TestGetAllBrandsNamesAsync()
        {
            IQueryable<Brand> brandMockQueryable = brandsData.BuildMock();
            this.brandRepository
                .Setup(p => p.GetAllAttached())
                .Returns(brandMockQueryable);

            IBrandService brandService = new BrandService(brandRepository.Object);

            IEnumerable<string> brandNames = await brandService.GetAllBrandsNamesAsync();
            Assert.IsTrue(brandNames.Any());
            Assert.That(brandNames.Count(), Is.EqualTo(brandsData.Count));
        }
    }
}

