using Application.Commands;
using Domain.Entities;
using MediatR;
using Repository.Base;

namespace Application.Handlers
{
    public sealed class HC_Test3: IRequestHandler<C_Test3, int>
    {
        public async Task<int> Handle(C_Test3 request, CancellationToken cancellationToken)
        {       
            using (IUnitOfWork unitOfWork=new UnitOfWork( new EShopContext ()))
            {
                IRepository<Category> categoryRepo = unitOfWork.Repository<Category>();
                IRepository<Product> productRepo = unitOfWork.Repository<Product>();

                Category category = new Category
                {
                    Description = "Test Category2",
                    Name = "CAT12",
                };
                categoryRepo.Add(category);

                Product product = new Product
                {
                    Name = "Test Product31312",
                    Price = 123,
                    Quantity = 13,
                    Category = category
                };
                productRepo.Add(product);

                await unitOfWork.SaveAsync();
            }
                    

            Console.WriteLine("HANDLER 3");
            return await Task.FromResult(0);
        }
    }
}