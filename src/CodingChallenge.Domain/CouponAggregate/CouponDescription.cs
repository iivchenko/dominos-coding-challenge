namespace CodingChallenge.Domain.CouponAggregate;

public sealed record CouponDescription
{
    public readonly int MaxLength = 250;

    public CouponDescription(string value)
    {
        Value = value;

        throw new NotImplementedException();
    }

    public string Value { get; private set; }
}
