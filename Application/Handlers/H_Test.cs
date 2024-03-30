using Application.Commands;
using MediatR;

namespace Application.Handlers
{
    public sealed class H_Test : IRequestHandler<C_Test, string>
    {
        public Task<string> Handle(C_Test request, CancellationToken cancellationToken)
        {
            return Task.FromResult($"{request.Name} {request.LastName} is {request.Age} years old 1");
        }
    }
}