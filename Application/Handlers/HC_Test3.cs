using Application.Commands;
using Domain.Entities;
using MediatR;
using Repository.Base;

namespace Application.Handlers
{
    public sealed class HC_Test3(IUnitOfWork _unitOfWork) : IRequestHandler<C_Test3, Category>
    {
        public async Task<Category> Handle(C_Test3 request, CancellationToken cancellationToken)
        {
            try
            {
                var categoryRepo = _unitOfWork.Repository<Category>();
                var productRepo = _unitOfWork.Repository<Product>();
                Category category = await categoryRepo.GetByIdAsync(request.CategoryId);
                Product prod = new Product
                {
                    Category = category,
                    Name = request.Name,
                    Price = request.Price,
                    Quantity = request.Quantity,
                };
                await productRepo.AddAsync(prod);
                var result = await categoryRepo.GetByIdAsync(category.Id, x => x.Products);

                await _unitOfWork.SaveAsync();
                return result;
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                throw;
            }
        }
    }
}