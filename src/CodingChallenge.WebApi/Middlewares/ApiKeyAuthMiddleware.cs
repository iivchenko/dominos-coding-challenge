namespace CodingChallenge.WebApi.Middlewares;

public sealed class ApiKeyAuthMiddleware
{
    private const string Header = "X-API-KEY";
    private const string ApiKeyConfigurationName = "X-API-KEY";

    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(Header, out var extractedKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API key is missing");
            return;
        }

        var apiKey = _configuration.GetValue<string>(ApiKeyConfigurationName);

        if (!apiKey.Equals(extractedKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid API key");
            return;
        }

        await _next(context);
    }
}
