namespace CodingChallenge.Domain.CouponAggregate;

public static class CouponProductCodesExtensions
{
    public static CouponProductCodes ToProductCodes(this IEnumerable<string> productcodes)
    {
        return new CouponProductCodes(productcodes);
    }
}
