using AutoMapper;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces.Repositorys;
using CleanArchitectureExample.Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CleanArchitectureExample.Application.Features.Commands.RequestShippingCommand;
using static CleanArchitectureExample.Application.Features.Queries.RequestShippingQuery;

namespace CleanArchitectureExample.Application.Features.Handlers
{
    public class RequestShippingHandler : 
        IRequestHandler<CreateRequestShippingCommand, RequestShipping>,
        IRequestHandler<GetListRequestShipping, List<RequestShipping>>,
        IRequestHandler<GetRequestShippingByMarketQuery, IEnumerable<RequestShipping>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RequestShippingHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<RequestShipping?> Handle(CreateRequestShippingCommand request, CancellationToken cancellationToken)
        {
            // 1. Kiểm tra Recipient có tồn tại không
            List<Recipient> recipient = await _unitOfWork.RecipientRepository.GetByConditionAsync(r =>
                r.Name == request.Recipient.Name &&
                r.Address == request.Recipient.Address &&
                r.Phone == request.Recipient.Phone
            );

            Recipient recipientAdd = new Recipient();
            if (recipient.Count == 0)
            {
                recipientAdd.Name = request.Recipient.Name;
                recipientAdd.Address = request.Recipient.Address;
                recipientAdd.Phone = request.Recipient.Phone;
               
                await _unitOfWork.RecipientRepository.AddAsync(recipientAdd);
                await _unitOfWork.CompleteAsync(); // Phải lưu để có RecipientId
            }

            // 2. Kiểm tra sản phẩm có tồn tại không
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                throw new Exception("Sản phẩm không tồn tại");
            }

            var shippingInfo = new ShippingInfo
            {
                SendMethod = request.SendMethod,
                Note = request.Note,
                DateSend = request.DateSend,
                ShippingPartnerId = request.ShippingPartnerId
            };

            await _unitOfWork.ShippingInfoRepository.AddAsync(shippingInfo);
            await _unitOfWork.CompleteAsync(); // Phải lưu để có ShippingInfoId

            // 3. Tính TotalPrice
            double totalPrice = product.Price * request.Quantity;

            // 4. Tạo RequestShipping
            RequestShipping requestShipping = new RequestShipping
            {
                NgayChotDon = request.NgayChotDon,
                UserProfileUserId = request.CreateBy,
                RecipientId = recipientAdd.RecipientId == 0 ? recipient[0].RecipientId : recipientAdd.RecipientId, // Gán Id đã có
                ProductId = request.ProductId,
                PageId = request.PageId,
                Quantity = request.Quantity,
                TotalPrice = totalPrice, // Tính tự động
                Status = "Pending",
                NgayDoiSoat = DateTime.UtcNow,
                ShippingInfoId = shippingInfo.ShippingInfoId
            };

            await _unitOfWork.RequestShippingRepositories.AddAsync(requestShipping);
            await _unitOfWork.CompleteAsync(); // Phải lưu để có RequestShippingId

            // 5. Tạo ShippingInfo (chưa có ShippingInfoId)
           

            // 6. Gán ShippingInfoId vào RequestShipping và cập nhật
            

            return requestShipping;
        }

        public async Task<List<RequestShipping>> Handle(GetListRequestShipping request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.RequestShippingRepositories.GetAllAsync();
        }

        public async Task<IEnumerable<RequestShipping>> Handle(GetRequestShippingByMarketQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.RequestShippingRepositories.GetByMarketIdAsync(request.MarketId);
        }
    }
}
