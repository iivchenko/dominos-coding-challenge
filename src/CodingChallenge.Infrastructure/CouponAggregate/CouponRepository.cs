using CodingChallenge.Application.Common;
using CodingChallenge.Domain.CouponAggregate;
using Microsoft.EntityFrameworkCore;

namespace CodingChallenge.Infrastructure.CouponAggregate;

// iivc comment:
// if I have more time I will implement Unit Of Work
public sealed class CouponRepository : ICouponRepository
{
    private readonly ApplicationDbContext _context;

    public CouponRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Coupon?> FindById(Guid id)
    {
        return await _context.Coupons.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Coupon?> FindByCode(CouponCode code)
    {
        return await _context.Coupons.SingleOrDefaultAsync(x => x.Code.Value == code.Value);
    }

    public async Task<Coupon> Create(Coupon coupon)
    {
        var entity = _context.Coupons.Add(coupon).Entity;

        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Coupon> Update(Coupon coupon)
    {
        var entity = _context.Coupons.Update(coupon).Entity;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new CodingChallengeApplicationExcepton($"Can't update entity with id '{coupon.Id}' due to concurrency issues!");
        }

        return entity;
    }
}
