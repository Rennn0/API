using Application.Commands;
using Application.Notifications;
using Domain.Entities;
using MediatR;
using Repository.Base;

namespace Application.Handlers
{
    public sealed class HC_Test2(IUnitOfWork _unitOfWork) : IRequestHandler<C_Test2, int>
    {
        public async Task<int> Handle(C_Test2 request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Repository<Category>().AddAsync(new Category { Description = request.Description, Name = request.Name });
            await _unitOfWork.SaveAsync();
            return 0;
        }
    }
}