using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public sealed class C_Test3 : IRequest<Category>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
    }
}