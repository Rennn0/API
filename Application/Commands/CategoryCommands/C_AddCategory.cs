using MediatR;
using Repository.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Category
{
	public sealed class C_AddCategory : IRequest<CategoryDto>
	{
		public string Name { get; set; }

		public string Description { get; set; }
	}
}