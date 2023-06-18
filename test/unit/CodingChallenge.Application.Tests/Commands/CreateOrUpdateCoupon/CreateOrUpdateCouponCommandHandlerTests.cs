using CodingChallenge.Application.Commands.CreateOrUpdateCoupon;
using CodingChallenge.Domain.CouponAggregate;
using FluentAssertions;
using Moq;

namespace CodingChallenge.Application.Tests.Commands.CreateOrUpdateCoupon;

public sealed class CreateOrUpdateCouponCommandHandlerTests
{
    private const string Name = "Name";
    private const string Description = "Description";
    private const string Code = "CODE_01";
    private const int MaxUsages = 10;
    private const decimal Price = 100.0m;
    private const string ProductCode1 = "Product_01";
    private const string ProductCode2 = "Product_02";

    private readonly CreateOrUpdateCouponCommandHandler _sut;
    private readonly Mock<ICouponRepository> _couponRepositoryMock;

    public CreateOrUpdateCouponCommandHandlerTests()
    {
        _couponRepositoryMock = new Mock<ICouponRepository>();
        _sut = new CreateOrUpdateCouponCommandHandler(_couponRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_CouponDoesntExist_CreateNewCoupon()
    {
        // Arrange
        var coupon = CreateCoupon();
        var command = new CreateOrUpdateCouponCommand(
            coupon.Id,
            coupon.Name, 
            coupon.Description, 
            coupon.Code, 
            coupon.Price,
            coupon.Usage.MaxUsages,
            coupon.Usage.Usages,
            coupon.ProductCodes.Values);

        _couponRepositoryMock
            .Setup(x => x.Create(It.IsAny<Coupon>()))
            .ReturnsAsync(coupon);

        // Act
        var response = await _sut.Handle(command, CancellationToken.None);

        response.Status.Should().Be(CreateOrUpdateCouponCommandResponseStatus.Created);
        response.Id.Should().Be(coupon.Id);
        response.Name.Should().Be(coupon.Name);
        response.Description.Should().Be(coupon.Description);
        response.Code.Should().Be(coupon.Code);
        response.Price.Should().Be(coupon.Price);
        response.MaxUsages.Should().Be(coupon.Usage.MaxUsages);
        response.Usages.Should().Be(coupon.Usage.Usages);
        response.ProductCodes.Should().BeEquivalentTo(coupon.ProductCodes.Values);
    }

    [Fact]
    public async Task Handle_CouponExist_UpdateExistingCoupon()
    {
        // Arrange
        var expectedName = "NewName";
        var expectedDescription = "NewDescription";
        var expectedPrice = 666.0m;
        var expectedMaxUsages = 2;
        var expectedUsages = 1;
        var expectedProductCodes = new[] { "Product_3", "Product_4" };

        var coupon = CreateCoupon();
        var command = new CreateOrUpdateCouponCommand(
            coupon.Id,
            expectedName,
            expectedDescription,
            coupon.Code,
            expectedPrice,
            expectedMaxUsages,
            expectedUsages,
            expectedProductCodes);

        _couponRepositoryMock
            .Setup(x => x.FindById(coupon.Id))
            .ReturnsAsync(coupon);

        _couponRepositoryMock
            .Setup(x => x.Update(coupon))
            .ReturnsAsync(coupon);

        // Act
        var response = await _sut.Handle(command, CancellationToken.None);

        response.Status.Should().Be(CreateOrUpdateCouponCommandResponseStatus.Updated);
        response.Id.Should().Be(coupon.Id);
        response.Name.Should().Be(expectedName);
        response.Description.Should().Be(expectedDescription);
        response.Code.Should().Be(coupon.Code);
        response.Price.Should().Be(expectedPrice);
        response.MaxUsages.Should().Be(expectedMaxUsages);
        response.Usages.Should().Be(expectedUsages);
        response.ProductCodes.Should().BeEquivalentTo(expectedProductCodes);
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
