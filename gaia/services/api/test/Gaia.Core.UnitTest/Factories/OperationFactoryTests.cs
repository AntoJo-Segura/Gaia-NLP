using FluentAssertions;
using Gaia.Core.Entities;
using Gaia.Core.Enums;
using Gaia.Core.Factories;
using System;
using Xunit;

namespace Gaia.Core.UnitTest.Factories
{
    public class OperationFactoryTests
    {
        private IOperationFactory _operationFactory;

        public OperationFactoryTests()
        {
            _operationFactory = new OperationFactory();
        }

        [Fact]
        public void Create_ThroguhtFactoryWithValidParameters_ReturnOperation()
        {
            // Arrange
            OperationId id = OperationId.Create(Guid.NewGuid());
            string inputFile = "test_file";
            Operation operation = _operationFactory.Create(id, inputFile);

            // Act

            // Assert
            operation.Should().NotBeNull();
            operation.Id.Should().Be(id);
            operation.Status.Should().Be(Status.Pending);
            operation.CreatedAt.Date.Should().Be(DateTime.UtcNow.Date);
            operation.InputFile.Should().Be(inputFile);
        }

        [Fact]
        public void Create_ThroguhtFactoryWithInValidIdParameter_ThrowArgumentNullException()
        {
            // Arrange
            OperationId id = null;
            string file = "test_file";

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => _operationFactory.Create(id, file));
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        public void Create_ThroguhtFactoryWithInValidFileParameter_ThrowArgumentException(string file, Type exceptionType)
        {
            // Arrange
            OperationId id = OperationId.Create(Guid.NewGuid());

            // Act

            // Assert
            Assert.Throws(exceptionType, () => _operationFactory.Create(id, file));
        }
    }
}
