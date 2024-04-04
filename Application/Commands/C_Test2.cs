using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public sealed class C_Test2 : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}