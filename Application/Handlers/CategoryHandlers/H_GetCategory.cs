using Application.Commands.CategoryCommands;
using Domain.Entities;
using MediatR;
using Repository.Base;
using Repository.DTOs;

namespace Application.Handlers.CategoryHandlers
{
	public sealed class H_GetCategory(IUnitOfWork _unit) : IRequestHandler<C_GetCategory, CategoryDto>
	{
		public async Task<CategoryDto> Handle(C_GetCategory request, CancellationToken cancellationToken)
		{
			try
			{
				Category category = await _unit.Repository<Category>().GetByIdAsync(request.Id, x => x.Products);
				CategoryDto dto;
				if (category.IsDeleted)
					dto = new CategoryDto()
					{
						Description = "CONTENT DELETED"
					};
				else
					dto = new CategoryDto()
					{
						Description = category.Description,
						Id = category.Id,
						Name = category.Name,
						Products = category.Products.Select(x => new ProductDto
						{
							Name = x.Name,
							Id = x.Id,
							CategoryId = x.CategoryId,
							Price = x.Price,
						}).ToList()
					};

				return dto;
			}
			catch
			{
				_unit.RollBack();
				throw;
			}
		}
	}
}