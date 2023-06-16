using CodingChallenge.Domain.Common;
using CodingChallenge.Domain.CouponAggregate;

namespace CodingChallenge.Domain.Tests.CouponAggregate;

public sealed class CouponProductCodesTests
{
    [Fact]
    public void Constructor_ValueIsNull_Throws()
    {
        // Arrange
        Action act = () => new CouponProductCodes(null);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage("A coupon product codes can't be null or empty!");
    }

    [Fact]
    public void Constructor_ValueIsEmpty_Throws()
    {
        // Arrange
        Action act = () => new CouponProductCodes(Enumerable.Empty<string>());

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage("A coupon product codes can't be null or empty!");
    }

    [Fact]
    public void Constructor_ValueContainsDuplicates_Throws()
    {
        // Arrange 
        var productCodes = new[]
        {
            "Product1",
            "Product2",
            "Product3",
            "Product1", // dublicate
            "Product3"  // dublicate
        };

        Action act = () => new CouponProductCodes(productCodes);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage($"A coupon product codes have duplicates: Product1, Product3!");
    }

    [Fact]
    public void Constructor_ValidValue_SetsValueProperty()
    {
        // Arrange 
        var productCodes = new[]
        {
            "Product1",
            "Product2",
            "Product3"
        };

        // Act 
        var sut = new CouponProductCodes(productCodes);

        // Assert
        sut.Values.Should().BeEquivalentTo(productCodes);
    }
}