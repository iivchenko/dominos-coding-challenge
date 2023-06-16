using CodingChallenge.Domain.Common;
using CodingChallenge.Domain.CouponAggregate;

namespace CodingChallenge.Domain.Tests.CouponAggregate;

public sealed class CouponNameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_EmptyValue_Throws(string value)
    {
        // Arrange
        Action act = () => new CouponName(value);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage("A coupon name can't be null or empty!");
    }

    [Fact]
    public void Constructor_ValueExceedsMaxLength_Throws()
    {
        // Arrange
        var faker = new Faker();
        var name = faker.Random.String(CouponName.MaxLength + 1);

        Action act = () => new CouponName(name);

        //Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage($"A coupon name '{name}' with length '{name.Length}' exceeds maximum allowed length of '{CouponName.MaxLength}' characters.!");
    }

    [Fact]
    public void Constructor_ValidValue_SetsValueProperty()
    {
        // Arrange
        var faker = new Faker();
        var value = faker.Random.String(CouponName.MaxLength);

        // Act
        var sut = new CouponName(value);
        // Assert
        sut.Value.Should().Be(value);
    }
}
