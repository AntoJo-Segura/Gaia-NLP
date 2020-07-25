using FluentAssertions;
using Gaia.Core.Entities;
using Gaia.Core.Enums;
using System;
using Xunit;

namespace Gaia.Core.UnitTest.Entities
{
    public class OperationTests
    {
        [Fact]
        public void Create_WithDefaultValues_ReturnOperation()
        {
            // Arrange
            Operation operation = new Operation();

            // Act

            // Assert
            operation.Should().NotBeNull();
        }

        [Fact]
        public void SetStatus_ChangeToProcessed_ReturnValidStatus()
        {
            // Arrange
            Operation operation = new Operation();

            // Act
            operation.SetStatus(Status.Processed);

            // Assert
            operation.Should().NotBeNull();
        }

        [Fact]
        public void SetStatus_ChangeToPendingWhenIsInProcessed_ThrowInvalidOperationException()
        {
            // Arrange
            Operation operation = new Operation();

            // Act
            operation.SetStatus(Status.Processed);

            // Assert
            Assert.Throws<InvalidOperationException>(() => operation.SetStatus(Status.Pending));
        }
    }
}
