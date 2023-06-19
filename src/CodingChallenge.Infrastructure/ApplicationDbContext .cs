using System.Reflection;
using CodingChallenge.Domain.CouponAggregate;
using Microsoft.EntityFrameworkCore;

namespace CodingChallenge.Infrastructure;

// iivc comment:
// I choosed SQL+EF setup as for me it is the most easiest and fastest way
// as well it is the most easiest way to run locally e.g. for CosmosDb or 
// Table Storage the emulator should be installed locally.
// 
// I think on production I would give preference to Table Storage
// as it is cheeper than SQL server and more than enough for current 
// acceptance criteria.
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Coupon> Coupons => Set<Coupon>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
