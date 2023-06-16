namespace CodingChallenge.Domain.CouponAggregate;

// iivc comment:
// Doesn't make sense to keep this type as 'record'
// as default compare/hash overriden but I will keep it
// for consistency with other object values
public sealed record CouponCode : IEquatable<CouponCode>
{
    public CouponCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("A coupon code can't be null or empty!");
        }

        Value = value;
    }

    public string Value { get; private set; }

    public bool Equals(CouponCode? other)
    {
        return other != null && string.Equals(Value, other.Value, StringComparison.InvariantCultureIgnoreCase);
    }

    public override int GetHashCode()
    {
        return string.GetHashCode(Value, StringComparison.InvariantCultureIgnoreCase);
    }
}
