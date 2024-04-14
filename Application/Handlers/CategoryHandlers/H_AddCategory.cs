using Application.Commands.Category;
using Domain.Entities;
using MediatR;
using Repository.Base;
using Repository.DTOs;

namespace Application.Handlers.CategoryHandlers
{
	public sealed class H_AddCategory(IUnitOfWork _unit) : IRequestHandler<C_AddCategory, CategoryDto>
	{
		public async Task<CategoryDto> Handle(C_AddCategory request, CancellationToken cancellationToken)
		{
			Category category = new()
			{
				Description = request.Description,
				Name = request.Name,
			};
			try
			{
				await _unit.Repository<Category>().AddAsync(category);
				await _unit.SaveAsync();
				return new CategoryDto
				{
					Name = category.Name,
					Description = category.Description,
					Id = category.Id
				};
			}
			catch
			{
				_unit.RollBack();
				throw;
			}
		}
	}
}