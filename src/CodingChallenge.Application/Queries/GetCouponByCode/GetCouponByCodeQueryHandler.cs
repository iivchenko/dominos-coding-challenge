using CodingChallenge.Application.Common;
using CodingChallenge.Domain.CouponAggregate;

namespace CodingChallenge.Application.Queries.GetCouponByCode;

public sealed class GetCouponByCodeQueryHandler : IRequestHandler<GetCouponByCodeQuery, GetCouponByCodeQueryResponse>
{
    private readonly ICouponRepository _couponRepository;

    public GetCouponByCodeQueryHandler(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<GetCouponByCodeQueryResponse> Handle(GetCouponByCodeQuery request, CancellationToken cancellationToken)
    {
        var coupon = await _couponRepository.FindByCode(request.Code);

        if (coupon == null)
        {
            throw new CodingChallengeApplicationExcepton($"Coupon with code '{request.Code}' doesn't exist!");
        }

        return new GetCouponByCodeQueryResponse(
            coupon.Id,
            coupon.Name,
            coupon.Description,
            coupon.Code,
            coupon.Price,
            coupon.Usage.MaxUsages,
            coupon.Usage.Usages,
            coupon.ProductCodes.Values);
    }
}
