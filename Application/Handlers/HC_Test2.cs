using Application.Commands;
using Application.Notifications;
using MediatR;

namespace Application.Handlers
{
    public sealed class HC_Test2(IMediator _mediator) : IRequestHandler<C_Test2, int>
    {
        public async Task<int> Handle(C_Test2 request, CancellationToken cancellationToken)
        {
            Console.WriteLine("HANDLER 2");

            Console.WriteLine("PUBLISHING --------");

            CancellationTokenSource ct = new();
            _ = _mediator.Publish(new N_Test(), ct.Token);

            Console.WriteLine("DONE PUBLISH--------");

            await Task.Delay(1500, cancellationToken);
            ct.Cancel();

            return 0;
        }
    }
}