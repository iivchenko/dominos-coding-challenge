﻿using CodingChallenge.Domain.CouponAggregate;

namespace CodingChallenge.Application.Commands.CreateOrUpdateCoupon;

// iivc comment:
// I would remove ID from Create action as the ID must be generated by system or storage
// but not by Client. Unfortunateally I can't change APIs so I have to workaround the case
// with extra validations.
public sealed class CreateOrUpdateCouponCommandHandler : IRequestHandler<CreateOrUpdateCouponCommand, CreateOrUpdateCouponCommandResponse>
{
    private readonly ICouponRepository _couponRepository;

    public CreateOrUpdateCouponCommandHandler(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    public async Task<CreateOrUpdateCouponCommandResponse> Handle(CreateOrUpdateCouponCommand request, CancellationToken cancellationToken)
    {
        if (await _couponRepository.FindById(request.Id) is Coupon coupon)
        {
            coupon.UpdateName(request.Name);
            coupon.UpdateDescription(request.Description);
            coupon.UpdatePrice(request.Price);
            coupon.UpdateMaxUsages(request.MaxUsages);
            coupon.UpdateProductCodes(request.ProductCodes.ToProductCodes());

            if (request.Usages > coupon.Usage.Usages)
            {
                coupon.IncrementUsage();
            }
            else if (request.Usages < coupon.Usage.Usages)
            {
                coupon.DecrementUsage();
            }

            return MapResponse(await _couponRepository.Update(coupon), CreateOrUpdateCouponCommandResponseStatus.Updated);            
        }
        else
        {
            var newCoupon = Coupon.Create(
                request.Id,
                request.Name,
                request.Description,
                request.Code,
                request.Price,
                request.MaxUsages,
                request.ProductCodes);

            return MapResponse(await _couponRepository.Create(newCoupon), CreateOrUpdateCouponCommandResponseStatus.Created);
        }
    }

    private static CreateOrUpdateCouponCommandResponse MapResponse(Coupon coupon, CreateOrUpdateCouponCommandResponseStatus status)
    {
        return new CreateOrUpdateCouponCommandResponse(
            status,
            coupon.Id,
            coupon.Name,
            coupon.Description, 
            coupon.Code,
            coupon.Price,
            coupon.Usage.MaxUsages,
            coupon.Usage.Usages,
            coupon.ProductCodes.Select(x => x.Value));
    }
}
