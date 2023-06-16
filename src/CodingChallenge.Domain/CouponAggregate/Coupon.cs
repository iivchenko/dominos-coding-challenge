namespace CodingChallenge.Domain.CouponAggregate;

public sealed class Coupon : IAggregateRoot<CouponId>
{
    private Coupon(
        CouponId id, 
        CouponName name, 
        CouponDescription description, 
        CouponCode code, 
        CouponPrice price, 
        CouponUsage usage,
        CouponProductCodes productCodes)
    {
        Id = id;
        Name = name;
        Description = description;
        Code = code;
        Price = price;
        Usage = usage;
        ProductCodes = productCodes;
    }

    public CouponId Id { get; private set; }
    public CouponName Name { get; private set; }
    public CouponDescription Description { get; private set; }
    public CouponCode Code { get; private set; }
    public CouponPrice Price { get; private set; }
    public CouponUsage Usage { get; private set; }
    public CouponProductCodes ProductCodes { get; private set; }

    public void UpdateName(CouponName name)
    {
        throw new NotImplementedException();
    }

    public void UpdateDescription(CouponDescription description)
    {
        throw new NotImplementedException();
    }

    public void UpdatePrice(CouponPrice price)
    {
        throw new NotImplementedException();
    }

    public void UpdateMaxUsage(int usage)
    {
        // iivc comment: 
        // Requirenments
        // When MaxUsages is set to 0, it means that the coupon can be applied an infinite number of times.

        throw new NotImplementedException();
    }

    public void IncrementUsage()
    {
        // iivc comment: 
        // Requirenments
        // Usages can only be incremented / decremented by 1 at a time.

        throw new NotImplementedException();
    }

    public void DecrementUsage()
    {
        // iivc comment: 
        // Requirenments
        // Usages can only be incremented / decremented by 1 at a time.

        throw new NotImplementedException();
    }

    public static Coupon Create(Guid id, string name, string description, string code, decimal price, int maxUsages, IEnumerable<string> productCodes)
    {
        throw new NotImplementedException();
    }
}
