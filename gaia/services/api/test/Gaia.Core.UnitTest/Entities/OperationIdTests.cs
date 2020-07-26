using System;
using FluentAssertions;
using Gaia.Core.Entities;
using Xunit;

namespace Gaia.Core.UnitTest.Entities
{
    public class OperationIdTests
    {
        [Fact]
        public void Should_ThrowArgumentException_When_CreateOperationIdWithDefaultGuid()
        {
            // Arrrange

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => OperationId.Create(Guid.Empty));
        }

        [Fact]
        public void Should_CreateOperationId_When_GuidIsValid()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            // Act
            var operationId = OperationId.Create(id);

            // Assert
            operationId.Should().NotBeNull();
            operationId.Id.Should().Be(id);
        }
    }
}
