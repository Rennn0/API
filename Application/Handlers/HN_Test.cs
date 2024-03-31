using Application.Notifications;
using MediatR;

namespace Application.Handlers
{
    public sealed class HN_Test : INotificationHandler<N_Test>
    {
        public async Task Handle(N_Test notification, CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken);
            Console.WriteLine("WE GOT NOTIFICATION 1");
        }
    }

    public sealed class HN_Test2 : INotificationHandler<N_Test>
    {
        public async Task Handle(N_Test notification, CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
            Console.WriteLine("WE GOT NOTIFICATION 2");
        }
    }
}