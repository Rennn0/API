using MediatR;

namespace Application.Commands.CategoryCommands
{
	public sealed class C_DeleteCategory : IRequest<object>
	{
		public int Id { get; set; }
	}
}