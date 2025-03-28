﻿using AutoMapper;
using CleanArchitectureExample.Application.DTOs;
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
        IRequestHandler<GetProductByIdQuery, ProductDTO?>,
        IRequestHandler<CreateProductCommand, int>,
        IRequestHandler<DeleteProductCommand, bool>,
        IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProductDTO?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

            var productDto = _mapper.Map<ProductDTO>(product);

            return productDto;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                ProductName = request.Name,
                Description = request.Description,
                CreateAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), // Format chuẩn
                Price = request.Price,
                Markets = new List<Market>() // Khởi tạo danh sách Market
            };

            // Nếu request có danh sách MarketIds, thêm vào Product
            if (request.MarketIds != null && request.MarketIds.Any())
            {
                var markets = await _unitOfWork.MarketRepositories
                    .GetByConditionAsync(m => request.MarketIds.Contains(m.MarketId));

                product.Markets.AddRange(markets);
            }

            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            return product.ProductId;
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
