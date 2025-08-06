using Microsoft.Extensions.Logging;
using Ramazan.Mediator;

namespace Bookstore.Application.Abstractions.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Handling {RequestName} with data: {@Request}", typeof(TRequest).Name, request);

        var response = await next();

        logger.LogInformation("Handled {RequestName} with response: {@Response}", typeof(TRequest).Name, response);

        return response;
    }
}
