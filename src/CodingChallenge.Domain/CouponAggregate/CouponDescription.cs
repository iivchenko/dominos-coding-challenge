namespace CodingChallenge.Domain.CouponAggregate;

public sealed record CouponDescription
{
    public static readonly int MaxLength = 250;

    public CouponDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("A coupon description can't be null or empty!");
        }

        if (value.Length > MaxLength)
        {
            throw new DomainException($"A coupon description '{value}' with length '{value.Length}' exceeds maximum allowed length of '{MaxLength}' characters.!");
        }

        Value = value;
    }

    public string Value { get; private set; }
}
