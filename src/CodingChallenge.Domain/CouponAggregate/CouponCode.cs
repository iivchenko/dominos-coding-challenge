namespace CodingChallenge.Domain.CouponAggregate;

public sealed record CouponCode
{
    // iivc comment: 
    // Requirenments
    // The coupon code is unique and immutable once created.
    // The coupon codes are case insensitive.
    public CouponCode(string value)
    {
        Value = value;

        throw new NotImplementedException();
    }

    public string Value { get; private set; }
}
