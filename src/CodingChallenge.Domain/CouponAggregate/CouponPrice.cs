namespace CodingChallenge.Domain.CouponAggregate;

public sealed record CouponPrice
{
    public CouponPrice(decimal value)
    {
        if (value <= 0.0m)
        {
            throw new DomainException($"A coupon price (which is '{value}') can't be 0 or negative value!");
        }

        Value = value;
    }

    public decimal Value { get; private set; }
}
