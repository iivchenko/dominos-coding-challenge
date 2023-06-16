namespace CodingChallenge.Domain.CouponAggregate;

public sealed record CouponName
{
    public static readonly int MaxLength = 25;

    public CouponName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("A coupon name can't be null or empty!");
        }

        if (value.Length > MaxLength)
        {
            throw new DomainException($"A coupon name '{value}' with length '{value.Length}' exceeds maximum allowed length of '{MaxLength}' characters.!");
        }

        Value = value;
    }

    public string Value { get; private set; }
}
