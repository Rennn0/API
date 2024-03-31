using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace Application.Middlewares
{
    public sealed class TracingBehavior<TRequest, TResponse>(ILogger<TracingBehavior<TRequest, TResponse>> _logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopWatch = Stopwatch.StartNew();
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");
            _logger.LogInformation($"Request: {JsonSerializer.Serialize(request)}");

            var response = await next();
            stopWatch.Stop();

            _logger.LogInformation($"Response: {response}");
            _logger.LogInformation($"Handled {typeof(TRequest).Name} in {stopWatch.ElapsedMilliseconds}ms");

            return response;
        }
    }
}