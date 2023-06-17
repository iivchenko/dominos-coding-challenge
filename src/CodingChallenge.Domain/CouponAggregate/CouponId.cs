namespace CodingChallenge.Domain.CouponAggregate;

public sealed record CouponId
{
    public CouponId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("A coupon id can't be empty!");
        }

        Value = value;
    }

    public Guid Value { get; private set; }

    public static implicit operator Guid(CouponId id)
    {
        return id.Value;
    }

    public static implicit operator CouponId(Guid id)
    {
        return new CouponId(id);
    }
}
