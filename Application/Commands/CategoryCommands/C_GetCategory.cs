using MediatR;
using Repository.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.CategoryCommands
{
	public sealed class C_GetCategory : IRequest<CategoryDto>
	{
		public int Id { get; set; }
	}
}