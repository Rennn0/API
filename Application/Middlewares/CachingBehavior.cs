using Application.OtherUtils;
using Application.Validations;
using MediatR;

namespace Application.Middlewares
{
    public sealed class CachingBehavior<Request, Response> : IPipelineBehavior<Request, Response>
        where Request : class, IRequest<Response>
    {
        private Dictionary<Request, Response> Cache { get; set; } = new Dictionary<Request, Response>(new ObjectComparer<Request>());

        public async Task<Response> Handle(Request request, RequestHandlerDelegate<Response> next, CancellationToken cancellationToken)
        {
            if (Cache.TryGetValue(request, out Response? value))
                return value;

            var result = await next();
            Cache.Add(request, result);
            Console.WriteLine($"Cached total - {Cache.Count}");
            Console.WriteLine($"values size - {ObjectSize.Get(Cache.Values)} bytes");
            Console.WriteLine($"keys size - {ObjectSize.Get(Cache.Keys)} bytes");
            return result;
        }
    }
}