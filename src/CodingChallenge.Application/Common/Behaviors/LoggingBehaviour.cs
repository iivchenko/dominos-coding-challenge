using Microsoft.Extensions.Logging;

namespace CodingChallenge.Application.Common.Behaviors;

// iivc comment:
// Logging is critical for production application. I didn't 
// add any logging in the project for reassons:
// - There is no sense to log in controllers as with poper telemetry support 
// (e.g. Azure Application Insights) it will be neabled automatically
// - Excessive logging may cause performance issues
// - The Application uses cases so primitive that there is no need for extra logging
//
// But just for an example I introduced this behavior on how the generic logging approach could be handled
public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("Request started: {Name} {@Request}", requestName, request);

        try
        {
            var response = await next();

            _logger.LogInformation("Request finished: {Name} {@Response}", requestName, response);

            return response;

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Request fail: {Name}", requestName);

            throw;
        }       
    }
}
