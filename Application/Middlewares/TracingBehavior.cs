using MediatR;

namespace Application.Middlewares
{
    public sealed class TracingBehavior<Request, Response> : IPipelineBehavior<Request, Response>
        where Request : IRequest<Response>
    {
        public async Task<Response> Handle(Request request, RequestHandlerDelegate<Response> next, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Tracing log : {request}");
            var response = await next();
            Console.WriteLine($"Tracing log : {response}");
            return response;
        }
    }
}