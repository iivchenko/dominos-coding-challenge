namespace CodingChallenge.Domain.CouponAggregate;

public sealed record CouponUsage
{
    public CouponUsage(int maxUsages, int usages)
    {
        if (maxUsages < 0)
        {
            throw new DomainException($"A coupon max usages '{maxUsages}' should be zero or greater!");
        }

        if (usages < 0)
        {
            throw new DomainException($"A coupon usages '{usages}' should be zero or greater!");
        }

        if (maxUsages > 0 && usages > maxUsages)
        {
            throw new DomainException($"A coupon usages '{usages}' should be less or equal then maximum usage '{maxUsages}'!");
        }

        MaxUsages = maxUsages;
        Usages = usages;
    }

    public int MaxUsages { get; private set; }

    public int Usages { get; private set; }
}
