using Application.Commands.CategoryCommands;
using Domain.Entities;
using MediatR;
using Repository.Base;
using Repository.DTOs;

namespace Application.Handlers.CategoryHandlers
{
	public sealed class H_UpdateCategory(IUnitOfWork _unit) : IRequestHandler<C_UpdateCategory, CategoryDto>
	{
		public async Task<CategoryDto> Handle(C_UpdateCategory request, CancellationToken cancellationToken)
		{
			try
			{
				var repo = _unit.Repository<Category>();

				Category category = await repo.GetByIdAsync(request.Id);
				category.Name = request.Name;
				category.Description = request.Description;
				category.ModifiedAt = DateTimeOffset.Now.ToUnixTimeMilliseconds();

				repo.Update(category);
				await repo.SaveAsync();

				return new CategoryDto
				{
					Description = category.Description,
					Name = category.Name,
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