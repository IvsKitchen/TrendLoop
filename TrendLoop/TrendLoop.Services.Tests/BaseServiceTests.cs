using TrendLoop.Services.Data;
using TrendLoop.Services.Data.Interfaces;
namespace TrendLoop.Services.Tests
{
    public class BaseServiceTests
    {
        
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task TestIsGuidValid()
        {
            IBaseService baseService = new BaseService();
            string emptyString = String.Empty;

            // Test valid case
            Guid parsedGuidValid = Guid.NewGuid();
            bool isGuidValid = baseService.IsGuidValid("02C82C25-C238-4EFC-97CB-83D511F1D360", ref parsedGuidValid);
            Assert.IsTrue(isGuidValid);

            // Test empty string case
            Guid parsedGuidEmptyString = Guid.NewGuid();
            bool isGuidValidEmptyString = baseService.IsGuidValid(emptyString, ref parsedGuidEmptyString);
            Assert.IsFalse(isGuidValidEmptyString);

            // Test whitespace case
            Guid parsedGuidWhiteSpace = Guid.NewGuid();
            bool isGuidValidWhiteSpace = baseService.IsGuidValid("", ref parsedGuidWhiteSpace);
            Assert.IsFalse(isGuidValidWhiteSpace);

            // Test invalid GUID case
            Guid parsedInvalidGuid = Guid.NewGuid();
            bool isGuidValidInvalidInput = baseService.IsGuidValid("test", ref parsedInvalidGuid);
            Assert.IsFalse(isGuidValidEmptyString);
        }
    }
}

