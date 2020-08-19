using Xunit;
using Moq;
using DotNetBuild.Infrastructure.Repositories.Interfaces;
using DotNetBuild.Services;
using System.Collections.Generic;

namespace DotNetBuildTests.Services
{
    public class CacheServiceTests
    {
        Mock<ICacheDataRepository> MockCacheDataRepository = null;
     public CacheServiceTests()
        {
            MockCacheDataRepository = new Mock<ICacheDataRepository>();
        }

         [Fact]
        public async System.Threading.Tasks.Task Get_PositiveAsync()
        {
            // Assign
            string ExpectedResult = "Sample Cache Response";
            MockCacheDataRepository.Setup(x => x.Get(It.IsAny<string>())).ReturnsAsync(ExpectedResult);
            // Act
            CacheService cacheService = new CacheService(MockCacheDataRepository.Object);
            string ActualResult = await cacheService.Get("Sample Key");
            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
}

         [Fact]
        public async System.Threading.Tasks.Task Post_PositiveAsync()
        {
            // Assign
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["SampleKey"] = "SampleValue";
            string ExpectedResult = "Data Inserted Succesfully !!"; 
            MockCacheDataRepository.Setup(x => x.Post(It.IsAny<Dictionary<string, string>>())).ReturnsAsync(ExpectedResult);
            // Act
            CacheService cacheService = new CacheService(MockCacheDataRepository.Object);
            string ActualResult = await cacheService.Post(data);
            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
}

         [Fact]
        public async System.Threading.Tasks.Task Put_PositiveAsync()
        {
            // Assign
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["SampleKey"] = "SampleValue";
            string ExpectedResult = $"Data Updated Succesfully !!";
            MockCacheDataRepository.Setup(x => x.Put(It.IsAny<Dictionary<string, string>>())).ReturnsAsync(ExpectedResult);
            // Act
            CacheService cacheService = new CacheService(MockCacheDataRepository.Object);
            string ActualResult = await cacheService.Put(data);
            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
        }

         [Fact]
        public async System.Threading.Tasks.Task Delete_PositiveAsync()
        {
            // Assign
            string key = "Sample Key";
            string ExpectedResult = $"Data Deleted Succesfully with ID: {key}"; ;
            MockCacheDataRepository.Setup(x => x.Delete(It.IsAny<string>())).ReturnsAsync(ExpectedResult);
            // Act
            CacheService cacheService = new CacheService(MockCacheDataRepository.Object);
            string ActualResult = await cacheService.Delete(key);
            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
        }
    }
}
