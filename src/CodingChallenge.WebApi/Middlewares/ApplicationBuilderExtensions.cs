using CodingChallenge.WebApi.Middlewares;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApiKeyAuthentication(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ApiKeyAuthMiddleware>();

        return builder;
    }

    public static IApplicationBuilder UseAppExceptionHandling(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionHandlingMiddleware>();

        return builder;
    }
}
