using MediatR;

namespace Application.Commands
{
    public sealed class C_Test2 : IRequest<int>
    {
        public int Id { get; set; }
    }
}