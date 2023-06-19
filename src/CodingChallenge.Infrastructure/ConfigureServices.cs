using CodingChallenge.Domain.CouponAggregate;
using CodingChallenge.Infrastructure;
using CodingChallenge.Infrastructure.CouponAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICouponRepository, CouponRepository>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CodingChallengeDb"));
        }
        else
        {
            services
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        return services;
    }
}
