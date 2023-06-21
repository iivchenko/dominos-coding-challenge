using CodingChallenge.Application.Common;
using CodingChallenge.Domain.CouponAggregate;

namespace CodingChallenge.Application.Queries.GetCouponById;

public sealed class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, GetCouponByIdQueryResponse>
{
    private readonly ICouponRepository _couponRepository;

    public GetCouponByIdQueryHandler(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<GetCouponByIdQueryResponse> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _couponRepository.FindById(request.Id) is Coupon coupon)
        {
            return new GetCouponByIdQueryResponse(
                coupon.Id,
                coupon.Name,
                coupon.Description,
                coupon.Code,
                coupon.Price,
                coupon.Usage.MaxUsages,
                coupon.Usage.Usages,
                coupon.ProductCodes.Select(x => x.Value));
        }

        throw new CodingChallengeApplicationExcepton($"Coupon with id '{request.Id}' doesn't exist!");
    }
}

