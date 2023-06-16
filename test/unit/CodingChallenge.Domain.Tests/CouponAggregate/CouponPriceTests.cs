using CodingChallenge.Domain.Common;
using CodingChallenge.Domain.CouponAggregate;

namespace CodingChallenge.Domain.Tests.CouponAggregate;

public sealed class CouponPriceTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]

    public void Constructor_ValueIsZeroOrNegatie_Throws(decimal value)
    {
        // Arrange
        Action act = () => new CouponPrice(value);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage($"A coupon price (which is '{value}') can't be 0 or negative value!");
    }

    [Fact]
    public void Constructor_ValidValue_SetsValueProperty()
    {
        // Arrange
        var faker = new Faker();
        var value = faker.Random.Decimal(0.01m);

        // Act
        var sut = new CouponPrice(value);

        // Assert
        sut.Value.Should().Be(value);
    }
}
