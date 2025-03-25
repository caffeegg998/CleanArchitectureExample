using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AutoMapper;
using CleanArchitectureExample.Application.DTOs;
using CleanArchitectureExample.Domain.Entities;

namespace CleanArchitectureExample.Application.MapperProfiles
{
    public class RequestShippingProfile : Profile
    {
        public RequestShippingProfile()
        {
            // Ánh xạ từ Entity (RequestShipping) sang DTO (RequestShippingDTO)
            CreateMap<RequestShipping, RequestShippingDTO>()
                .ForMember(dest => dest.NguoiTao, opt => opt.MapFrom(src => src.UserProfile)) // Map UserProfile
                .ForMember(dest => dest.ProductDTO, opt => opt.MapFrom(src => src.Product)) // Map ShippingInfo
                .ForMember(dest => dest.ShippingInfoDTO, opt => opt.MapFrom(src => src.ShippingInfo));

            //// Ánh xạ từ DTO (RequestShippingDTO) sang Entity (RequestShipping)
            //CreateMap<RequestShippingDTO, RequestShipping>()
            //    .ForMember(dest => dest.UserProfileUserId, opt => opt.MapFrom(src => src.UserProfile.UserId)) // Map UserProfileUserId
            //    .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => src.Recipient.RecipientId)) // Map RecipientId
            //    .ForMember(dest => dest.PageId, opt => opt.MapFrom(src => src.Page.PageId)) // Map PageId
            //    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId)) // Map ProductId
            //    .ForMember(dest => dest.ShippingInfoId, opt => opt.MapFrom(src => src.ShippingInfo.ShippingInfoId)); // Map ShippingInfoId

            CreateMap<Page, PageDTO>();
            CreateMap<ShippingInfo, ShippingInfoDTO>();
            CreateMap<ShippingPartner, ShippingPartnerDTO>();
        }
    }
}
