using System.Collections.ObjectModel;

namespace CodingChallenge.Domain.CouponAggregate;

public static class CouponProductCodesExtensions
{
    public static IReadOnlyCollection<CouponProductCode> ToProductCodes(this IEnumerable<string> productcodes)
    {
        var codes = productcodes.Select(x => new CouponProductCode(x)).ToArray();

        return new ReadOnlyCollection<CouponProductCode>(codes);
    }
}
