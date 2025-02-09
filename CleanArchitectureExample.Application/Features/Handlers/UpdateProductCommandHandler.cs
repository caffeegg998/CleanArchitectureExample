using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Application.Features.Commands;
using CleanArchitectureExample.Infrastructure.Persistence.Repositories.Interfaces;
using CleanArchitectureExample.Infrastructure.Persistence.UnitOfWork;
using MediatR;

namespace CleanArchitectureExample.Application.Features.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
            if (product == null) return false;

            product.Name = request.Name;
            product.Price = request.Price;

            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.CompleteAsync();  // Lưu thay đổi vào DB
            return true;
        }
    }
}
