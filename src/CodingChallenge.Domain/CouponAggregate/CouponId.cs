namespace CodingChallenge.Domain.CouponAggregate;

public sealed record CouponId
{
    public CouponId(Guid value)
    {
        Value = value;

        throw new NotImplementedException();
    }

    public Guid Value { get; private set; }
}
