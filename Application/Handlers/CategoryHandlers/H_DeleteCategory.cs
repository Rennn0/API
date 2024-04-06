using Application.Commands.CategoryCommands;
using Domain.Entities;
using MediatR;
using Repository.Base;

namespace Application.Handlers.CategoryHandlers
{
	public sealed class H_DeleteCategory(IUnitOfWork _unit) : IRequestHandler<C_DeleteCategory, Object>
	{
		public async Task<object> Handle(C_DeleteCategory request, CancellationToken cancellationToken)
		{
			try
			{
				var repo = _unit.Repository<Category>();

				Category category = await repo.GetByIdAsync(request.Id);
				category.ModifiedAt = DateTimeOffset.Now.ToUnixTimeMilliseconds();
				category.IsDeleted = true;

				repo.Update(category);
				await repo.SaveAsync();

				return new
				{
					message = $"Id-{request.Id}, Name-{category.Name} deleted successfuly"
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