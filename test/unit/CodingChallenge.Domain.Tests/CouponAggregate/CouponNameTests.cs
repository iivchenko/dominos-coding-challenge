namespace CodingChallenge.Domain.Tests.CouponAggregate;

public sealed class CouponNameTests
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
    public void Constructor_ValueExceedsMaxLength_Throws()
    {
        // Arrange
        // Act
        // Assert
    }
}
