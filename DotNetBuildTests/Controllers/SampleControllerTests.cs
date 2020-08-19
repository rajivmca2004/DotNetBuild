using Microsoft.Extensions.Logging;
using Xunit;
using Moq;
using DotNetBuild.Controllers;
using DotNetBuild.Services.Interfaces;
using DotNetBuild.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBuildTest
{
    public class SampleControllerTests
    {
        Mock<ILogger<SampleController>> MockLogger = new Mock<ILogger<SampleController>>();
        private Mock<ISampleService> MockSampleService = null;
        Mock<ICacheService> MockCacheService = null;
        Mock<IMessageSubscriberService> MockMessageSubscriberService = null;
        Mock<IMessagePublisherService> MockMessagePublisherService = null;
        SampleController sampleController = null;
        public SampleControllerTests()
        {
            MockSampleService = new Mock<ISampleService>();
            MockCacheService = new Mock<ICacheService>();
            MockMessageSubscriberService = new Mock<IMessageSubscriberService>();
            MockMessagePublisherService = new Mock<IMessagePublisherService>();
            sampleController = new SampleController(MockLogger.Object
                   , MockSampleService.Object
                   , MockCacheService.Object
                   , MockMessageSubscriberService.Object, MockMessagePublisherService.Object
                                            );
        }

         [Fact]
        public void GetAllSamples_Positive()
        {
            // Arrange

            List<Sample> MockSampleData = new List<Sample> {
                                                                        new Sample{
                                                                            Id = 1,
                                                                            Name = "Sample",
                                                                            Details = "Test Data"
                                                                        }
               };
            MockSampleService.Setup(x => x.Get()).Returns(MockSampleData);

            // Act
            IEnumerable<Sample> ActualSampleData = sampleController.GetAllSamples();

            // Assert
            var ActualResult = ActualSampleData.Where(x => x.Id == MockSampleData[0].Id).ToList()[0];
            Assert.Equal(MockSampleData.Count, ActualSampleData.Count());
            Assert.Equal(MockSampleData[0].Id, ActualResult.Id);
            Assert.Equal(MockSampleData[0].Name, ActualResult.Name);
            Assert.Equal(MockSampleData[0].Details, ActualResult.Details);
        }

         [Fact]
        public void GetAllSamples_Negative()
        {
            // Arrange
            List<Sample> MockSampleData = new List<Sample>();
            MockSampleService.Setup(x => x.Get()).Returns(MockSampleData);

            // Act
            IEnumerable<Sample> ActualSampleData = sampleController.GetAllSamples();

            // Assert
            Assert.True(ActualSampleData.Count() == 0);
            Assert.Equal(MockSampleData.Count, ActualSampleData.Count());
        }

         [Fact]
        public void GetSample_Positive()
        {
            // Arrange
            Sample MockSampleData = new Sample
            {
                Id = 1,
                Name = "Sample",
                Details = "Test Data"
            };
            MockSampleService.Setup(x => x.Get(It.IsAny<int>())).Returns(MockSampleData);

            // Act
            Sample ActualSampleData = sampleController.GetSample(1);

            // Assert
            Assert.Equal(MockSampleData.Id, ActualSampleData.Id);
            Assert.Equal(MockSampleData.Name, ActualSampleData.Name);
            Assert.Equal(MockSampleData.Details, ActualSampleData.Details);
        }

         [Fact]
        public void GetSample_Negative()
        {
            // Arrange
            Sample MockSampleData = new Sample();
            MockSampleService.Setup(x => x.Get(It.IsAny<int>())).Returns(MockSampleData);

            // Act
            Sample ActualSampleData = sampleController.GetSample(1);

            // Assert
            Assert.Equal(MockSampleData.Id, ActualSampleData.Id);
            Assert.Equal(MockSampleData.Name, ActualSampleData.Name);
            Assert.Equal(MockSampleData.Details, ActualSampleData.Details);
        }

         [Fact]
        public void AddSample_Positive()
        {
            // Assing
            int ID = 77;
            Sample MockSampleObject = new Sample
            {
                Id = ID,
                Name = "Sample Test Name",
                Details = "Sample Test Details"
            };
            string ExpectedResult = $"Data Inserted Succesfully with ID: {ID}";
            MockSampleService.Setup(x => x.Post(It.IsAny<Sample>())).Returns(ExpectedResult);

            // Act
            string ActualResult = sampleController.AddSample(MockSampleObject);

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
        }

         [Fact]
        public void AddSample_Negative()
        {
            // Assing
            int ID = 77;
            Sample MockSampleObject = new Sample
            {
                Id = ID,
                Name = "Sample Test Name",
                Details = "Sample Test Details"
            };
            string ExpectedResult = $"Data already exists with ID: {ID}. Please try Put Operation";
            MockSampleService.Setup(x => x.Post(It.IsAny<Sample>())).Returns(ExpectedResult);

            // Act
            string ActualResult = sampleController.AddSample(MockSampleObject);

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
        }

         [Fact]
        public void UpdateSample_Positive()
        {
            // Assing
            int ID = 77;
            Sample MockSampleObject = new Sample
            {
                Id = ID,
                Name = "Sample Test Name",
                Details = "Sample Test Details"
            };
            string ExpectedResult = $"Data Updated Succesfully with ID: {ID}";
            MockSampleService.Setup(x => x.Put(It.IsAny<Sample>())).Returns(ExpectedResult);

            // Act
            string ActualResult = sampleController.UpdateSample(MockSampleObject);

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
        }

         [Fact]
        public void UpdateSample_Negative()
        {
            // Assing
            int ID = 77;
            Sample MockSampleObject = new Sample
            {
                Id = ID,
                Name = "Sample Test Name",
                Details = "Sample Test Details"
            };
            string ExpectedResult = $"Data doesnot exists with ID: {ID}. Please try Post Operation";
            MockSampleService.Setup(x => x.Put(It.IsAny<Sample>())).Returns(ExpectedResult);

            // Act
            string ActualResult = sampleController.UpdateSample(MockSampleObject);

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
        }

         [Fact]
        public void DeleteSample_Positive()
        {
            // Arrange
            int ID = 7;
            string ExpectedResult = $"Data Deleted Succesfully with ID: {ID}";
            MockSampleService.Setup(x => x.Delete(It.IsAny<int>())).Returns(ExpectedResult);

            // Act
            string ActualResult = sampleController.DeleteSample(ID);

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
        }

         [Fact]
        public void DeleteSample_Negative()
        {
            // Arrange
            int ID = 7;
            string ExpectedResult = $"Data doesnot exists with ID: {ID}. Please try Post Operation";
            MockSampleService.Setup(x => x.Delete(It.IsAny<int>())).Returns(ExpectedResult);

            // Act
            string ActualResult = sampleController.DeleteSample(ID);

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
        }


         [Fact]
        public async Task GetCacheData_Positive()
        {
            // Arrange
            string ExpectedResult = "Cache Result";

            // Act
            MockCacheService.Setup(x => x.Get(It.IsAny<string>())).ReturnsAsync(ExpectedResult);
            string ActualResult = await sampleController.GetCacheData("sample Key");

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
        }

         [Fact]
        public async Task AddCacheData_Positive()
        {
            // Arrange
            string ExpectedResult = "Cache Result";
            Dictionary<string, string> Dict = new Dictionary<string, string>();
            Dict.Add("sampleKey", "sampleValue");
            // Act
            MockCacheService.Setup(x => x.Post(It.IsAny<Dictionary<string, string>>())).ReturnsAsync(ExpectedResult);
            string ActualResult = await sampleController.AddCacheData(Dict);

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
        }

         [Fact]
        public async Task DeleteCacheData_Positive()
        {
            // Arrange
            string ExpectedResult = "Cache Result";

            // Act
            MockCacheService.Setup(x => x.Delete(It.IsAny<string>())).ReturnsAsync(ExpectedResult);
            string ActualResult = await sampleController.DeleteCacheData("sample Key");

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
        }


         [Fact]
        public void Subscribe_Positive()
        {
            // Arrange
            string ExpectedResult = "Sample Result";
            MockMessageSubscriberService.Setup(x => x.Subscribe()).Returns(ExpectedResult);

            // Act
            var ActualResult = sampleController.Subscribe();

            // Assert
            Assert.Equal(ExpectedResult, ActualResult.Value);
        }

         [Fact]
        public void Publish_Positive()
        {
            // Arrange
            MockMessagePublisherService.Setup(x => x.Publish(It.IsAny<string>()));

            // Act
            sampleController.Publish("sample Message");

            // Assert
            // Not returning any value
        }

    }
}
