using MediatR;

namespace Application.Commands.CategoryCommands
{
	public sealed class C_DeleteCategory : IRequest<Object>
	{
		public int Id { get; set; }
	}
}