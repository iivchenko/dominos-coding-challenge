using CodingChallenge.Domain.Common;
using CodingChallenge.Domain.CouponAggregate;

namespace CodingChallenge.Domain.Tests.CouponAggregate;

public sealed class CouponCodeTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_EmptyValue_Throws(string value)
    {
        // Arrange
        Action act = () => new CouponCode(value);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage("A coupon code can't be null or empty!");
    }

    [Fact]
    public void Value_TwoDifferentCouponsWithValueInDifferentCasing_CouponsAreEqual()
    {
        // Arrange+Act
        var couponCode1 = new CouponCode("THE_CODE111");
        var couponCode2 = new CouponCode("the_code111");

        // Assert
        couponCode1.Should().Be(couponCode2, "Coupon code must be case insensitive");
    }

    [Fact]
    public void Constructor_ValidValue_SetsValueProperty()
    {
        // Arrange+Act
        var faker = new Faker();
        var code = faker.Random.String();
        var sut = new CouponCode(code);

        // Assert
        sut.Value.Should().Be(code);
    }
}
