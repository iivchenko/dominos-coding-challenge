using CodingChallenge.Domain.Common;
using CodingChallenge.Domain.CouponAggregate;

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

    [Fact]
    public void Create_IdIsEmpty_Throws()
    {
        // Arrange
        Action act = () => CreateCoupon(id: Guid.Empty);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage("A coupon id can't be empty!");
    }

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
        sut.Id.Should().Be(expectedId);
        sut.Name.Value.Should().Be(expectedName);
        sut.Description.Value.Should().Be(expectedDescription);
        sut.Code.Value.Should().Be(expectedCode);
        sut.Price.Value.Should().Be(expectedPrice);
        sut.Usage.MaxUsages.Should().Be(expectedMaxUsages);
        sut.Usage.Usages.Should().Be(expectedUsages);
        sut.ProductCodes.Should().BeEquivalentTo(expectedProductCodes.ToProductCodes());
    }

    [Fact]
    public void UpdateName_NameIsNull_Throws()
    {
        // Arrange
        var originalName = "Some Good Name";
        var sut = CreateCoupon(name: originalName);
        Action act = () => sut.UpdateName(null);

        // Act+Assert
        act
            .Should()
            .Throw<ArgumentNullException>()
            .WithParameterName("name");

        sut.Name.Value.Should().Be(originalName);
    }

    [Fact]
    public void UpdateMaxUsage_SameValue_NothingChanged()
    {
        // Arrange
        var originalMaxUsages = 10;
        var expectedMaxUsages = 10;
        var sut = CreateCoupon(maxUsages: originalMaxUsages);

        // Act
        sut.UpdateMaxUsages(expectedMaxUsages);

        // Assert
        sut.Usage.MaxUsages.Should().Be(expectedMaxUsages);
    }

    [Fact]
    public void UpdateMaxUsage_NewMaxUsagesIsZeroAndUsagesIs1_NewMaxUsagesApplied()
    {
        // Arrange
        var originalMaxUsages = 10;
        var expectedMaxUsages = 0;
        var sut = CreateCoupon(maxUsages: originalMaxUsages);

        sut.IncrementUsage();

        // Act
        sut.UpdateMaxUsages(expectedMaxUsages);

        // Assert
        sut.Usage.MaxUsages.Should().Be(expectedMaxUsages);
    }

    [Fact]
    public void UpdateMaxUsage_NewMaxUsagesIs1AndUsagesIs2_Throws()
    {
        // Arrange
        var originalMaxUsages = 2;
        var badMaxUsages = 1;
        var sut = CreateCoupon(maxUsages: originalMaxUsages);

        sut.IncrementUsage();
        sut.IncrementUsage();

        // Act
        Action act = () => sut.UpdateMaxUsages(badMaxUsages);

        // Assert
        act
           .Should()
           .Throw<DomainException>()
           .WithMessage($"Can't update max usages for Coupon with id '{sut.Id}' as expected max usages '{badMaxUsages}' less than actual usages '{sut.Usage.Usages}'!");
    }

    [Fact]
    public void UpdateProductCodes_ValueIsNull_Throws()
    {
        // Arrange
        var sut = CreateCoupon();

        Action act = () => sut.UpdateProductCodes(null);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage("A coupon product codes can't be null or empty!");
    }

    [Fact]
    public void UpdateProductCodes_ValueIsEmpty_Throws()
    {
        // Arrange
        var sut = CreateCoupon();

        Action act = () => sut.UpdateProductCodes(Enumerable.Empty<CouponProductCode>());

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage("A coupon product codes can't be null or empty!");
    }

    [Fact]
    public void UpdateProductCodes_ValueContainsDuplicates_Throws()
    {
        // Arrange 
        var productCodes = new[]
        {
            new CouponProductCode("Product1"),
            new CouponProductCode("Product2"),
            new CouponProductCode("Product3"),
            new CouponProductCode("Product1"), // dublicate
            new CouponProductCode("Product3")  // dublicate
        };

        var coupon = CreateCoupon();

        Action act = () => coupon.UpdateProductCodes(productCodes);

        // Act+Assert
        act
            .Should()
            .Throw<DomainException>()
            .WithMessage($"A coupon product codes have duplicates: Product1, Product3!");
    }

    [Fact]
    public void UpdateProductCodes_ValidValue_SetsValueProperty()
    {
        // Arrange 
        var productCodes = new[]
        {
            new CouponProductCode("Product4"),
            new CouponProductCode("Product5"),
            new CouponProductCode("Product6")
        };

        var sut = CreateCoupon();

        // Act 
        sut.UpdateProductCodes(productCodes);

        // Assert
        sut.ProductCodes.Should().BeEquivalentTo(productCodes);
    }

    private static Coupon CreateCoupon(
        Guid? id = null,
        string name = Name, 
        string description = Description, 
        string code = Code,
        int maxUsages = MaxUsages,
        decimal price = Price,
        params string[] productCodes)
    {
        return Coupon.Create(id ?? Guid.NewGuid(), name, description, code, price, maxUsages, productCodes.Any() ? productCodes : new[] { ProductCode1, ProductCode2 });
    }
}
