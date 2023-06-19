namespace CodingChallenge.Domain.CouponAggregate;

public sealed record CouponProductCode
{
    public CouponProductCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("A coupon product code can't be null or empty!");
        }

        Value = value;
    }

    public string Value { get; private set; }
}
