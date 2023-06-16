namespace CodingChallenge.Domain.CouponAggregate;

public sealed record CouponProductCodes
{
    public CouponProductCodes(IEnumerable<string> values)
    {
        Values = values;

        throw new NotImplementedException();
    }

    public IEnumerable<string> Values { get; private set; }
}
