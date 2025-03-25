using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Domain.Entities.Identity;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Application.DTOs.Account;
using CleanArchitectureExample.Domain.Enums;

namespace CleanArchitectureExample.Application.DTOs
{
    public class RequestShippingDTO
    {
        public int RequestShippingId { get; set; }
        public DateTime NgayChotDon { get; set; }
        public UserProfileDTO_Min NguoiTao { get; set; }
        public Recipient Recipient { get; set; }
        public PageDTO Page { get; set; }
        public ProductSummaryDTO ProductDTO { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public ShippingInfoDTO ShippingInfoDTO { get; set; }

        public RequestShippingStatusEnum Status { get; set; }
        public DateTime NgayDoiSoat { get; set; }
    }
}
