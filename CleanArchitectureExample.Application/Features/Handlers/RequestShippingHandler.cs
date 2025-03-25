using AutoMapper;
using CleanArchitectureExample.Application.DTOs;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Enums;
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
        IRequestHandler<CreateRequestShippingCommand, RequestShippingDTO>,
        IRequestHandler<GetListRequestShipping, List<RequestShipping>>,
        IRequestHandler<GetRequestShippingByMarketQuery, TotalRequestShippingByMarketDTO>,
        IRequestHandler<UpdateRequestShippingCommand ,RequestShippingDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RequestShippingHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<RequestShippingDTO?> Handle(CreateRequestShippingCommand request, CancellationToken cancellationToken)
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
                UserProfileUserId = request.CreatedBy,
                RecipientId = recipientAdd.RecipientId == 0 ? recipient[0].RecipientId : recipientAdd.RecipientId, // Gán Id đã có
                ProductId = request.ProductId,
                PageId = request.PageId,
                Quantity = request.Quantity,
                TotalPrice = totalPrice, // Tính tự động
                Status = RequestShippingStatusEnum.Pending,
                NgayDoiSoat = DateTime.UtcNow,
                ShippingInfoId = shippingInfo.ShippingInfoId
            };

            await _unitOfWork.RequestShippingRepositories.AddAsync(requestShipping);
            await _unitOfWork.CompleteAsync(); // Phải lưu để có RequestShippingId

            // 5. Tạo ShippingInfo (chưa có ShippingInfoId)


            // 6. Gán ShippingInfoId vào RequestShipping và cập nhật

            RequestShippingDTO requestShippingDTO = _mapper.Map<RequestShippingDTO>(requestShipping);

            return requestShippingDTO;
        }

        public async Task<List<RequestShipping>> Handle(GetListRequestShipping request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.RequestShippingRepositories.GetAllAsync();
        }

        public async Task<TotalRequestShippingByMarketDTO> Handle(GetRequestShippingByMarketQuery request, CancellationToken cancellationToken)
        {
            TotalRequestShippingByMarketDTO totalRequestShippingByMarketDTO = new TotalRequestShippingByMarketDTO();
            Market market = await _unitOfWork.MarketRepositories.GetByIdAsync(request.MarketId);

            if (market == null)
            {
                // Xử lý trường hợp không tìm thấy market
                throw new Exception("Market not found");
            }

            // Lấy giá trị MarketName từ đối tượng market
            string marketName = market.MarketName;
            totalRequestShippingByMarketDTO.MarketName = marketName;

            List<RequestShipping> requestShippings = await _unitOfWork.RequestShippingRepositories.GetByMarketIdAsync(request.MarketId);
            if(requestShippings.Count > 0)
            {
                List<RequestShippingDTO> productDto = _mapper.Map<List<RequestShippingDTO>>(requestShippings);
                totalRequestShippingByMarketDTO.TotalRequest = productDto.Count;
                totalRequestShippingByMarketDTO.ListRequestShipping = productDto;

                TotalRequestStatus totalRequestStatus = new TotalRequestStatus();
                totalRequestStatus.Pending = productDto.Count(x => x.Status == RequestShippingStatusEnum.Pending);
                totalRequestStatus.Processed = productDto.Count(x => x.Status == RequestShippingStatusEnum.Processed);
                totalRequestStatus.Shipped = productDto.Count(x => x.Status == RequestShippingStatusEnum.Shipped);
                totalRequestStatus.Delivered = productDto.Count(x => x.Status == RequestShippingStatusEnum.Delivered);
                totalRequestStatus.Cancelled = productDto.Count(x => x.Status == RequestShippingStatusEnum.Cancelled);
                totalRequestStatus.Returned = productDto.Count(x => x.Status == RequestShippingStatusEnum.Returned);
                totalRequestShippingByMarketDTO.TotalRequestStatus = totalRequestStatus;

            }

            return totalRequestShippingByMarketDTO;
        }

        public async Task<RequestShippingDTO> Handle(UpdateRequestShippingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                RequestShipping requestShipping = _mapper.Map<RequestShipping>(request.requestShippingDTO);
                RequestShipping requestShippingD = await _unitOfWork.RequestShippingRepositories.UpdateAsync(requestShipping);
                RequestShippingDTO requestShippingDTO = new RequestShippingDTO(); ;
                if (requestShippingD != null) {
                    requestShippingDTO = _mapper.Map<RequestShippingDTO>(requestShippingD);
                }
                await _unitOfWork.CompleteAsync();
                return requestShippingDTO;
            }
            catch (Exception ex) { 
                return new RequestShippingDTO();
            }
        }
    }
}
