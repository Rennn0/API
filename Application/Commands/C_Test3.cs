using MediatR;

namespace Application.Commands
{
    public sealed class C_Test3 : IRequest<int>
    {
        public int Id { get; set; }
    }
}