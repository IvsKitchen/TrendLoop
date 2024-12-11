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
    public class AttributeTypeServiceTests
    {
        // Subcategories
        private IList<Subcategory> subcategories;

        // AttributeTypes
        private IList<AttributeType> attributeTypesData;

        // SubcategoryAttributeType
        IList<SubcategoryAttributeType> attributeTypesForSubcategory;

        // AttributeType repository mock
        private Mock<IRepository<AttributeType, int>> attributeTypeRepository;

        // AttributeValue repository mock
        private Mock<IRepository<AttributeValue, int>> attributeValueRepository;

        [SetUp]
        public void Setup()
        {
            subcategories = new List<Subcategory>()
            {
                // Clothing subcategories
                new Subcategory { Id = 1, Name = "Dresses", CategoryId = 1 },
                // Shoes subcategories
                new Subcategory { Id = 2, Name = "Boots", CategoryId = 2 },
            };

            attributeTypesForSubcategory = new List<SubcategoryAttributeType>
            {
                // Dresses
                new SubcategoryAttributeType { AttributeTypeId = 1, SubcategoryId = 1},
                // Shoes
                new SubcategoryAttributeType { AttributeTypeId = 2, SubcategoryId = 2},
            };

            attributeTypesData = new List<AttributeType>()
            {
                new AttributeType { Id = 1, Name = "Clothing Size", SubcategoryAttributeTypes = new HashSet<SubcategoryAttributeType>(){attributeTypesForSubcategory[0]}},
                new AttributeType { Id = 2, Name = "Shoes Size", SubcategoryAttributeTypes = new HashSet<SubcategoryAttributeType>(){attributeTypesForSubcategory[1]}},
            };

            attributeTypeRepository = new Mock<IRepository<AttributeType, int>>();
            attributeValueRepository = new Mock<IRepository<AttributeValue, int>>();
        }

        [Test]
        public async Task TestGetAttributeTypesBySubcategoryIdAsync()
        {
            IQueryable<AttributeType> attributeTypesMockQueryable = attributeTypesData.BuildMock();
            this.attributeTypeRepository
                .Setup(p => p.GetAllAttached())
                .Returns(attributeTypesMockQueryable);

            IAttributeTypeService attributeTypeService = new AttributeTypeService(attributeTypeRepository.Object, attributeValueRepository.Object);

            IEnumerable<AttributeTypeInfoViewModel> attributeTypesModelsBySubcategory = await attributeTypeService.GetAttributeTypesBySubcategoryIdAsync(subcategories[0].Id);
            Assert.IsTrue(attributeTypesModelsBySubcategory.Any());
            Assert.That(attributeTypesModelsBySubcategory.Count(), Is.EqualTo(attributeTypesData
                                                                                                .Where(at => at.SubcategoryAttributeTypes
                                                                                                    .All(sat => sat.SubcategoryId == subcategories[0].Id))
                                                                                                    .ToList().Count));
        }
    }
}

