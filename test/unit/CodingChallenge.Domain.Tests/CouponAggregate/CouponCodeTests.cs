using CodingChallenge.Domain.CouponAggregate;
using FluentAssertions;

namespace CodingChallenge.Domain.Tests.CouponAggregate;

public sealed class CouponCodeTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_EmptyValue_Throws(string value)
    {
        // Arrange
        // Act
        // Assert
    }

    [Fact]
    public void Constructor_ValidValue_SetsValueProperty()
    {
        // Arrange
        // Act
        // Assert
    }

    [Fact]
    public void Value_TwoDifferentCouponsWithValueInDifferentCasing_CouponsAreEqual()
    {
        // Arrange
        // Act
        // Assert
    }
}
