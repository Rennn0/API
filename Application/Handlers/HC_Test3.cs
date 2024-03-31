using Application.Commands;
using MediatR;

namespace Application.Handlers
{
    public sealed class HC_Test3 : IRequestHandler<C_Test3, int>
    {
        public Task<int> Handle(C_Test3 request, CancellationToken cancellationToken)
        {
            Console.WriteLine("HANDLER 3");
            return Task.FromResult(0);
        }
    }
}