namespace CodingChallenge.Domain.CouponAggregate;

public sealed record CouponProductCodes
{
    public CouponProductCodes(IEnumerable<string> values)
    {
        if (values == null || !values.Any())
        {
            throw new DomainException("A coupon product codes can't be null or empty!");
        }

        var duplicates = values.GroupBy(x => x).Where(x => x.Count() > 1);

        if (duplicates.Any())
        {
            throw new DomainException($"""A coupon product codes have duplicates: {string.Join(", ", duplicates.Select(x => x.Key))}!""");
        }

        Values = values;
    }

    public IEnumerable<string> Values { get; private set; }
}
