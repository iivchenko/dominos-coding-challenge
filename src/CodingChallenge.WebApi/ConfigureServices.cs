using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services , IConfiguration configuration)
    {
        services
            .AddOutputCache(options =>
            {
                // iivc comment:
                // I don't have metrics how API is used (GET, PUT ratio and frequency)
                //
                // Also Coupon PUT endpoint/Domain is not reliable for extensive caching as 
                // system can accept Usages as plain integer, but requirenments forces to increment/decremetn
                // by ONE only, so extensive period of caching may introduce signifincant error rate for PUT
                //
                // I decided to use short experation to minimize PUT possible issues.
                // I decided to use Output Caching strategy as it is easy to implement compared to InMemoryCaching and
                // I don't expect clients to store/utilize coupons for a long time (Response Caching could be better here)
                // but I expect different new clients to fetch coupons
                if (TimeSpan.TryParse(configuration.GetValue<string>("OutputCache:Expiration"), out var expiration))
                {
                    options.AddBasePolicy(builder => builder.Expire(expiration));
                }
            })
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Coupon", Version = "v1" });
                c.AddSecurityDefinition("ApiKey",
                    new OpenApiSecurityScheme
                    {
                        Description = "ApiKey must appear in header",
                        Type = SecuritySchemeType.ApiKey,
                        Name = "X-API-KEY",
                        In = ParameterLocation.Header,
                        Scheme = "ApiKeyScheme"
                    });
                var key = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" },
                    In = ParameterLocation.Header
                };
                var requirement = new OpenApiSecurityRequirement { { key, new List<string>() } };
                c.AddSecurityRequirement(requirement);
            })
            .AddControllers();

        return services;
    }
}
