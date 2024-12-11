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
    public class AttributeValueServiceTests
    {
        // AttributeTypes
        private IList<AttributeType> attributeTypes;

        // AttributeTypes
        private IList<AttributeValue> attributeValuesData;

        // AttributeValue repository mock
        private Mock<IRepository<AttributeValue, int>> attributeValueRepository;

        [SetUp]
        public void Setup()
        {
            attributeTypes = new List<AttributeType>()
            {
                new AttributeType { Id = 1, Name = "Clothing Size"},
                new AttributeType { Id = 2, Name = "Shoes Size"},
            };

            // Values for Attribute "ClothingSize"
            attributeValuesData = new List<AttributeValue>()
            {
                new AttributeValue { Id = 1, Value = "M", AttributeTypeId = 1 },
                new AttributeValue { Id = 2, Value = "37", AttributeTypeId = 2 },
            };

            attributeValueRepository = new Mock<IRepository<AttributeValue, int>>();
        }

        [Test]
        public async Task TestGetAttributeValuesByAttributeTypeIdAsync()
        {
            IQueryable<AttributeValue> attributeValuesMockQueryable = attributeValuesData.BuildMock();
            this.attributeValueRepository
                .Setup(p => p.GetAllAttached())
                .Returns(attributeValuesMockQueryable);

            IAttributeValueService attributeTypeService = new AttributeValueService(attributeValueRepository.Object);

            IEnumerable<AttributeValueInfoViewModel> attributeValuesByAttributeTypes =
                await attributeTypeService.GetAttributeValuesByAttributeTypeIdAsync(attributeTypes[0].Id);
            Assert.IsTrue(attributeValuesByAttributeTypes.Any());
            Assert.That(attributeValuesByAttributeTypes.Count(), 
                Is.EqualTo(attributeValuesData.Where(av => av.AttributeTypeId == attributeTypes[0].Id).ToList().Count));
        }
    }
}

