using Application.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using Repository.Base;
using Repository.Exceptions;
using Repository.Extensions;
using System.Text.Json;

namespace Application.Handlers
{
    public sealed class HC_Test3(IUnitOfWork _unitOfWork) : IRequestHandler<C_Test3, Category>
    {
        public Task<Category> Handle(C_Test3 request, CancellationToken cancellationToken)
        {
            try
            {
                Category category = _unitOfWork.Repository<Category>().GetByIdAsync(request.CategoryId).Result;
                Product prod = new Product
                {
                    Category = category,
                    Name = request.Name,
                    Price = request.Price,
                    Quantity = request.Quantity,
                };
                _unitOfWork.Repository<Product>().AddAsync(prod).Wait(cancellationToken);
                category = _unitOfWork.Repository<Category>().GetByIdAsync(category.Id, x => x.Products).Result;
                _unitOfWork.Repository<Product>().SaveAsync();
                return Task.FromResult(category);
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                throw;
            }
        }
    }
}