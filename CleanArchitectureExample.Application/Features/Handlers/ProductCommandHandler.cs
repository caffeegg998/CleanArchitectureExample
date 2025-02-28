using CleanArchitectureExample.Application.Features.Queries;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CleanArchitectureExample.Application.Features.Commands.ProductCommand;

namespace CleanArchitectureExample.Application.Features.Handlers
{
    public class ProductCommandHandler :
        IRequestHandler<GetProductByIdQuery, Product?>,
        IRequestHandler<CreateProductCommand, int>,
        IRequestHandler<DeleteProductCommand, bool>,
        IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                
                ProductName = request.Name,
                Price = request.Price
            };

            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            return product.Id;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

                if (product == null)
                {
                    // Xử lý trường hợp sản phẩm không tồn tại
                    return false;
                }

                await _unitOfWork.ProductRepository.DeleteAsync(product);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public  async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
            if (product == null) return false;

            product.ProductName = request.Name;
            product.Price = request.Price;

            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.CompleteAsync();  // Lưu thay đổi vào DB
            return true;
        }
    }
}
