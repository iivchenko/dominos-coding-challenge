using CodingChallenge.Domain.Common;
using CodingChallenge.Domain.CouponAggregate;

namespace CodingChallenge.Domain.Tests.CouponAggregate;

public sealed class CouponUsageTests
{
    [Fact]
    public void Constructor_MaxUsagesIsLessThanZero_Throws()
    {
        // Arrange
        var fake = new Faker();
        var maxUsages = fake.Random.Int(int.MinValue, -1);
        Action act = () => new CouponUsage(maxUsages, 0);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage($"A coupon max usages '{maxUsages}' should be zero or greater!");
    }

    [Fact]
    public void Constructor_UsagesIsLessThanZero_Throws()
    {
        // Arrange
        var fake = new Faker();
        var usages = fake.Random.Int(int.MinValue, -1);
        Action act = () => new CouponUsage(0, usages);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage($"A coupon usages '{usages}' should be zero or greater!");
    }

    [Fact]
    public void Constructor_UsagesGreaterMaxUsagesAndMaxUsagesGreaterZero_Throws()
    {
        // Arrange
        var maxUsages = 1;
        var usages = 2;
        Action act = () => new CouponUsage(maxUsages, usages);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage($"A coupon usages '{usages}' should be less or equal then maximum usage '{maxUsages}'!");
    }

    [Fact]
    public void Constructor_UsagesGreaterMaxUsagesAndMaxUsagesEqualZero_MaxUsagesAndUsagesSetSuccessfulLy()
    {
        // Arrange
        var maxUsages = 0;
        var usages = 2;

        // Act
        var sut = new CouponUsage(maxUsages, usages);

        // Assert
        sut.MaxUsages.Should().Be(maxUsages);
        sut.Usages.Should().Be(usages);
    }

    [Fact]
    public void Constructor_UsagesLesserThanMaxUsagesAndMaxUsagesGreaterZero_MaxUsagesAndUsagesSetSuccessfulLy()
    {
        // Arrange
        var maxUsages = 3;
        var usages = 2;

        // Act
        var sut = new CouponUsage(maxUsages, usages);

        // Assert
        sut.MaxUsages.Should().Be(maxUsages);
        sut.Usages.Should().Be(usages);
    }
}