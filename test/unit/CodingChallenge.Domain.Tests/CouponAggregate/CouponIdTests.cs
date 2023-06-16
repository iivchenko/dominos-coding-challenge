using CodingChallenge.Domain.Common;
using CodingChallenge.Domain.CouponAggregate;

namespace CodingChallenge.Domain.Tests.CouponAggregate;

public sealed class CouponIdTests
{
    [Fact]
    public void Constructor_EmptyValue_Throws()
    {
        // Arrange
        Action act = () => new CouponId(Guid.Empty);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage("A coupon id can't be empty!");
    }

    [Fact]
    public void Constructor_ValidValue_SetsValueProperty()
    {
        // Arrange
        var value = Guid.NewGuid();

        // Act
        var sut = new CouponId(value);

        // Assert
        sut.Value.Should().Be(value);
    }
}
