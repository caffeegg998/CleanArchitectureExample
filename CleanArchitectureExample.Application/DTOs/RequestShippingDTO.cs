using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Domain.Entities.Identity;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Application.DTOs.Account;

namespace CleanArchitectureExample.Application.DTOs
{
    public class RequestShippingDTO
    {
        public int RequestShippingId { get; set; }
        public DateTime NgayChotDon { get; set; }
        public UserProfileDTO UserProfile { get; set; }
        public Recipient Recipient { get; set; }
        public Page Page { get; set; }
        public ProductSummaryDTO Product { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public ShippingInfoDTO ShippingInfo { get; set; }

        public string Status { get; set; }
        public DateTime NgayDoiSoat { get; set; }
    }
}
