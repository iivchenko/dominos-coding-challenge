namespace CodingChallenge.Domain.CouponAggregate;

public sealed record CouponUsage
{
    public CouponUsage(int maxUsage, int usage)
    {
        Max = maxUsage;
        Current = usage;

        throw new NotImplementedException();
    }

    public int Max { get; private set; }

    public int Current { get; private set; }
}
