namespace CodingChallenge.Domain.CouponAggregate;

public static class CouponProductCodesExtensions
{
    public static ICollection<CouponProductCode> ToProductCodes(this IEnumerable<string> productcodes)
    {
        var codes = productcodes.Select(x => new CouponProductCode(x)).ToArray();

        return codes.ToList();
    }
}
