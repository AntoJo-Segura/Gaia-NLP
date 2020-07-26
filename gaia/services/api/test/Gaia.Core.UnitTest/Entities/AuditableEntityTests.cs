using FluentAssertions;
using Gaia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Gaia.Core.UnitTest.Entities
{
    public class AuditableEntityTests
    {
        [Fact]
        public void Create_WithValidProperties_ReturnEntity()
        {
            // Arrange
            AuditableEntity entity = new TestAuditableEntity();

            // Act
            entity.CreatedAt = DateTime.UtcNow.Date;
            entity.UpdatedAt = DateTime.UtcNow.Date;

            // Assert
            entity.CreatedAt.Should().Be(DateTime.UtcNow.Date);
            entity.UpdatedAt.Should().Be(DateTime.UtcNow.Date);
        }
    }
}
