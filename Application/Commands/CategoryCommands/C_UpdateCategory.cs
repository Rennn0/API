using MediatR;
using Repository.DTOs;

namespace Application.Commands.CategoryCommands
{
	public sealed class C_UpdateCategory : IRequest<CategoryDto>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}