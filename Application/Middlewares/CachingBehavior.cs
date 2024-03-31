using Application.OtherUtils;
using Application.Validations;
using MediatR;

namespace Application.Middlewares
{
    public sealed class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
    {
        private Dictionary<TRequest, TResponse> Cache { get; set; } = new Dictionary<TRequest, TResponse>(new ObjectComparer<TRequest>());

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (Cache.TryGetValue(request, out TResponse? value))
                return value;

            var result = await next();
            Cache.Add(request, result);
            return result;
        }
    }
}