﻿using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services , IConfiguration configuration)
    {
        services
            .AddOutputCache(options =>
            {
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
