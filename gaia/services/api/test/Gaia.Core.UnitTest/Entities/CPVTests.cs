using System;
using System.Linq;
using FluentAssertions;
using Gaia.Core.Entities;
using Xunit;

namespace Gaia.Core.UnitTest.Entities
{
    public class CPVTests
    {
        [Fact]
        public void Create_WithValidValues_ReturnNewCPV()
        {
            // Arrange
            string code = "002";
            CPV cpv = new CPV(code, CPVType.Standard);

            // Act
            cpv.AddDescription("test_descriptions");

            // Assert
            cpv.Code.Should().Be(code);
            cpv.Type.Should().Be(CPVType.Standard);
            cpv.Descriptions.Count.Should().Be(1);
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        public void Create_ThrowArgumentException_CodeIsNull(string code, Type exceptionType)
        {
            // Arrange
            // Act
            //Assert
            Assert.Throws(exceptionType, () => new CPV(code, CPVType.Standard));
        }

        [Fact]

        public void AddDescription_AddNewDescriptionToList_DescriptionNotNulAndNotEmpty()
        {
            // Arrange
            CPV cpv = new CPV("002", CPVType.Standard);

            // Act
            cpv.AddDescription("test");

            // Assert
            cpv.Descriptions.FirstOrDefault().Should().Be("test");
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(ArgumentException))]
        public void AddDescription_ThrowArgumentException_DescriptionIsNullOrEmpty(string description, Type exceptionType)
        {
            // Arrange
            CPV cpv = new CPV("002", CPVType.Standard);
            // Act

            //Assert
            Assert.Throws(exceptionType, () => cpv.AddDescription(description));
        }
    }
}
