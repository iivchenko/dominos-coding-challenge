using Bogus.DataSets;
using CodingChallenge.Domain.Common;
using CodingChallenge.Domain.CouponAggregate;
using FluentAssertions;

namespace CodingChallenge.Domain.Tests.CouponAggregate;

// iivc comment: 
// I created only one unit test for Create NULL check (Create_NameIsNull_Throws)
// as all the other tests will be similar.
//
// I didn't create any extra tests for Create method to validate ranges, lengths etc.
// as those scenarios covered with appropriate value object tests.
//
// To save some time I created only one unit test for the Update methods NULL check (UpdateName_NameIsNull_Throws) 
// as all the other tests will be similar
//
// To save some time I skiped increment/decrement unit tests
//
// WARNING: Somebody can say that those are integration tests as we don't test Coupone AggregateRoot behavior
// but behavior of its Value Objects (which looks like integration) but on my opinion we unit test AGGREGATE
// where AggregateRoot and ValueObjects are indivisible parts of the aggregate. 
public sealed class CouponTests
{
    private const string Name = "Name";
    private const string Description = "Description";
    private const string Code = "CODE_01";
    private const int MaxUsages = 10;
    private const decimal Price = 100.0m;
    private const string ProductCode1 = "Product_01";
    private const string ProductCode2 = "Product_02";

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Create_NameIsNull_Throws(string name)
    {
        // Arrange
        Action act = () => CreateCoupon(name: name);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage("A coupon name can't be null or empty!");
    }

    [Fact]
    public void Create_AllConditionsAreMet_ReturnSuccessfullyCreatedCoupon()
    {
        // Arrange
        var expectedId = Guid.NewGuid();
        var expectedName = Name;
        var expectedDescription = Description;
        var expectedCode = Code;
        var expectedPrice = Price;
        var expectedMaxUsages = MaxUsages;
        var expectedUsages = 0;
        var expectedProductCodes = new[] { ProductCode1, ProductCode2 };

        // Act
        var sut = Coupon.Create(
            expectedId,
            expectedName,
            expectedDescription,
            expectedCode,
            expectedPrice,
            expectedMaxUsages,
            expectedProductCodes);

        // Assert
        sut.Id.Value.Should().Be(expectedId);
        sut.Name.Value.Should().Be(expectedName);
        sut.Description.Value.Should().Be(expectedDescription);
        sut.Code.Value.Should().Be(expectedCode);
        sut.Price.Value.Should().Be(expectedPrice);
        sut.Usage.MaxUsages.Should().Be(expectedMaxUsages);
        sut.Usage.Usages.Should().Be(expectedUsages);
        sut.ProductCodes.Values.Should().BeEquivalentTo(expectedProductCodes);
    }

    [Fact]
    public void UpdateName_NameIsNull_Throws()
    {
        // Arrange
        var originalName = "Some Good Name";
        var coupon = CreateCoupon(name: originalName);
        Action act = () => coupon.UpdateName(null);

        // Act+Assert
        act
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName("name");

        coupon.Name.Value.Should().Be(originalName);
    }

    [Fact]
    public void UpdateMaxUsage_SameValue_NothingChanged()
    {
        // Arrange
        var originalMaxUsages = 10;
        var expectedMaxUsages = 10;
        var coupon = CreateCoupon(maxUsages: originalMaxUsages);

        // Act
        coupon.UpdateMaxUsages(expectedMaxUsages);

        // Assert
        coupon.Usage.MaxUsages.Should().Be(expectedMaxUsages);
    }

    [Fact]
    public void UpdateMaxUsage_NewMaxUsagesIsZeroAndUsagesIs1_NewMaxUsagesApplied()
    {
        // Arrange
        var originalMaxUsages = 10;
        var expectedMaxUsages = 0;
        var coupon = CreateCoupon(maxUsages: originalMaxUsages);

        coupon.IncrementUsage();

        // Act
        coupon.UpdateMaxUsages(expectedMaxUsages);

        // Assert
        coupon.Usage.MaxUsages.Should().Be(expectedMaxUsages);
    }

    [Fact]
    public void UpdateMaxUsage_NewMaxUsagesIs1AndUsagesIs2_Throws()
    {
        // Arrange
        var originalMaxUsages = 2;
        var badMaxUsages = 1;
        var coupon = CreateCoupon(maxUsages: originalMaxUsages);

        coupon.IncrementUsage();
        coupon.IncrementUsage();

        // Act
        Action act = () => coupon.UpdateMaxUsages(badMaxUsages);

        // Assert
        act
           .Should()
           .Throw<DomainException>()
           .WithMessage($"Can't update max usages for Coupon with id '{coupon.Id}' as expected max usages '{badMaxUsages}' less than actual usages '{coupon.Usage.Usages}'!");
    }

    private static Coupon CreateCoupon(
        string name = Name, 
        string description = Description, 
        string code = Code,
        int maxUsages = MaxUsages,
        decimal price = Price,
        params string[] productCodes)
    {
        return Coupon.Create(Guid.NewGuid(), name, description, code, price, maxUsages, productCodes.Any() ? productCodes : new[] { ProductCode1, ProductCode2 });
    }
}
