using Xunit;
using Moq;
using DotNetBuild.Domain.Models;
using DotNetBuild.Domain.Models.Interfaces;
using DotNetBuild.Services;
using System.Collections.Generic;
using System.Linq;

namespace DotNetBuildTests.Services
{
    public class SampleServiceTests
    {
        Mock<ISampleDataRepository> MockSampleDataRepository = null;
    public SampleServiceTests()
        {
            MockSampleDataRepository = new Mock<ISampleDataRepository>();
        }

         [Fact]
        public void GetAll_PositiveTest()
        {
            // Assign
            List<Sample> MockSampleData = new List<Sample> {
                                                                        new Sample{
                                                                            Id = 1,
                                                                            Name = "Sample",
                                                                            Details = "Test Data"
                                                                        }
               };
            MockSampleDataRepository.Setup(x => x.Get()).Returns(MockSampleData);

            // Act
            SampleService sampleService = new SampleService(MockSampleDataRepository.Object);
            IEnumerable<Sample> ActualSampleData = sampleService.Get();

            // Assert
            var ActualResult = ActualSampleData.Where(x => x.Id == MockSampleData[0].Id).ToList()[0];

            Assert.Equal(MockSampleData.Count, ActualSampleData.Count());
            Assert.Equal(MockSampleData[0].Id, ActualResult.Id);
            Assert.Equal(MockSampleData[0].Name, ActualResult.Name);
            Assert.Equal(MockSampleData[0].Details, ActualResult.Details);
}

         [Fact]
        public void GetAll_Negative()
        {
            // Arrange
            List<Sample> MockSampleData = new List<Sample>();
            MockSampleDataRepository.Setup(x => x.Get()).Returns(MockSampleData);

            // Act
            SampleService sampleService = new SampleService(MockSampleDataRepository.Object);
            IEnumerable<Sample> ActualSampleData = sampleService.Get();

            // Assert
             Assert.True(ActualSampleData.Count() == 0);
            Assert.Equal(MockSampleData.Count, ActualSampleData.Count());
            
}

         [Fact]
        public void GetSingle_Positive()
        {
            // Arrange
            Sample MockSampleData = new Sample
            {
                Id = 1,
                Name = "Sample",
                Details = "Test Data"
            };
            MockSampleDataRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(MockSampleData);

            // Act
            SampleService sampleService = new SampleService(MockSampleDataRepository.Object);
            Sample ActualSampleData = sampleService.Get(1);

            // Assert
            Assert.Equal(MockSampleData.Id, ActualSampleData.Id);
            Assert.Equal(MockSampleData.Name, ActualSampleData.Name);
            Assert.Equal(MockSampleData.Details, ActualSampleData.Details);
        }

         [Fact]
        public void GetSingle_Negative()
        {
            // Arrange
            Sample MockSampleData = new Sample();
            MockSampleDataRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(MockSampleData);

            // Act
            SampleService sampleService = new SampleService(MockSampleDataRepository.Object);
            Sample ActualSampleData = sampleService.Get(1);

            // Assert
            Assert.Equal(MockSampleData.Id, ActualSampleData.Id);
            Assert.Equal(MockSampleData.Name, ActualSampleData.Name);
            Assert.Equal(MockSampleData.Details, ActualSampleData.Details);
}


         [Fact]
        public void Post_Positive()
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
            MockSampleDataRepository.Setup(x => x.Post(It.IsAny<Sample>())).Returns(ExpectedResult);

            // Act
            SampleService sampleService = new SampleService(MockSampleDataRepository.Object);
            string ActualResult = sampleService.Post(MockSampleObject);

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
}

         [Fact]
        public void Post_Negative()
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
            MockSampleDataRepository.Setup(x => x.Post(It.IsAny<Sample>())).Returns(ExpectedResult);

            // Act
            SampleService sampleService = new SampleService(MockSampleDataRepository.Object);
            string ActualResult = sampleService.Post(MockSampleObject);

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
}

         [Fact]
        public void Put_Positive()
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
            MockSampleDataRepository.Setup(x => x.Put(It.IsAny<Sample>())).Returns(ExpectedResult);

            // Act
            SampleService sampleService = new SampleService(MockSampleDataRepository.Object);
            string ActualResult = sampleService.Put(MockSampleObject);

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
            
}

         [Fact]
        public void Put_Negative()
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
            MockSampleDataRepository.Setup(x => x.Put(It.IsAny<Sample>())).Returns(ExpectedResult);

            // Act
            SampleService sampleService = new SampleService(MockSampleDataRepository.Object);
            string ActualResult = sampleService.Put(MockSampleObject);

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
}

         [Fact]
        public void Delete_Positive()
        {
            // Arrange
            int ID = 7;
            string ExpectedResult = $"Data Deleted Succesfully with ID: {ID}";
            MockSampleDataRepository.Setup(x => x.Delete(It.IsAny<int>())).Returns(ExpectedResult);

            // Act
            SampleService sampleService = new SampleService(MockSampleDataRepository.Object);
            string ActualResult = sampleService.Delete(ID);

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
}

         [Fact]
        public void Delete_Negative()
        {
            // Arrange
            int ID = 7;
            string ExpectedResult = $"Data doesnot exists with ID: {ID}. Please try Post Operation";
            MockSampleDataRepository.Setup(x => x.Delete(It.IsAny<int>())).Returns(ExpectedResult);

            // Act
            SampleService sampleService = new SampleService(MockSampleDataRepository.Object);
            string ActualResult = sampleService.Delete(ID);

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
        }
    }
}
