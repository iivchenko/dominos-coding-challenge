namespace CodingChallenge.Domain.CouponAggregate;

public interface ICouponRepository
{
    Task<Coupon?> FindById(Guid id);
    Task<Coupon?> FindByCode(CouponCode code);
    Task<Coupon> Create(Coupon coupon);
    Task<Coupon> Update(Coupon coupon);
}
