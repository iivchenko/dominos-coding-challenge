namespace CodingChallenge.Domain.CouponAggregate;

public sealed record CouponName
{
    public readonly int MaxLength = 25;

    public CouponName(string value)
    {
        Value = value;

        throw new NotImplementedException();
    }

    public string Value { get; private set; }
}
