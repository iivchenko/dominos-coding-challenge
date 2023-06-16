namespace CodingChallenge.Domain.CouponAggregate;

public sealed record CouponPrice
{
    public CouponPrice(decimal value)
    {
        Value = value;

        throw new NotImplementedException();
    }

    public decimal Value { get; private set; }
}
