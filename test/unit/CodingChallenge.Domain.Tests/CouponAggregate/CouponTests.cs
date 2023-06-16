using CodingChallenge.Domain.CouponAggregate;

namespace CodingChallenge.Domain.Tests.CouponAggregate;

// iivc comment: 
// I created only one unit test for Create NULL check (Create_NameIsNull_Throws)
// as all the other tests will be similar.
//
// I didn't create any extra tests for Create method to validate ranges, lengths etc.
// as those scenarios covered with appropriate value object tests.
//
// As well I created only one unit test for the Update methods NULL check (UpdateName_NameIsNull_Throws)
// as all the other tests will be similar.
//
// I skiped increment/decrement unit tests as those transiently covered in CouponUsage value object
public sealed class CouponTests
{
    [Fact]
    public void Create_NameIsNull_Throws()
    {
        // Arrange
        // Act
        // Assert
    }

    [Fact]
    public void Create_AllConditionsAreMet_ReturnSuccessfullyCreatedCoupon()
    {
        // Arrange
        // Act
        // Assert
    }

    [Fact]
    public void UpdateName_NameIsNull_Throws()
    {
        // Arrange
        // Act
        // Assert
    }

    [Fact]
    public void UpdateMaxUsage()
    {
        // Arrange
        // Act
        // Assert
    }
}
