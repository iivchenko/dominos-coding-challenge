using CodingChallenge.Domain.Common;
using CodingChallenge.Domain.CouponAggregate;

namespace CodingChallenge.Domain.Tests.CouponAggregate;

public sealed class CouponDescriptionTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_EmptyValue_Throws(string value)
    {
        // Arrange
        Action act = () => new CouponDescription(value);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage("A coupon description can't be null or empty!");
    }

    [Fact]
    public void Constructor_ValueExceedsMaxLength_Throws()
    {
        // Arrange
        var faker = new Faker();
        var value = faker.Random.String(CouponDescription.MaxLength + 1);

        Action act = () => new CouponDescription(value);

        //Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage($"A coupon description '{value}' with length '{value.Length}' exceeds maximum allowed length of '{CouponDescription.MaxLength}' characters.!");
    }

    [Fact]
    public void Constructor_ValidValue_SetsValueProperty()
    {
        // Arrange
        var faker = new Faker();
        var value = faker.Random.String(CouponDescription.MaxLength);

        // Act
        var sut = new CouponDescription(value);
        // Assert
        sut.Value.Should().Be(value);
    }
}
