using CodingChallenge.Domain.Common;
using CodingChallenge.Domain.CouponAggregate;

namespace CodingChallenge.Domain.Tests.CouponAggregate;

public sealed class CouponProductCodeTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_EmptyValue_Throws(string value)
    {
        // Arrange
        Action act = () => new CouponProductCode(value);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage("A coupon product code can't be null or empty!");
    }

    [Fact]
    public void Constructor_ValidValue_SetsValueProperty()
    {
        // Arrange
        var faker = new Faker();
        var value = faker.Random.String();

        // Act
        var sut = new CouponProductCode(value);

        // Assert
        sut.Value.Should().Be(value);
    }
}
