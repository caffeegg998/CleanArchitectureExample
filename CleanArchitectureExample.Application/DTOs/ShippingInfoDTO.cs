using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Enums;

namespace CleanArchitectureExample.Application.DTOs
{
    public class ShippingInfoDTO
    {
        public int ShippingInfoId { get; set; }
        public string SendMethod { get; set; }
        public string? TimeReceived { get; set; }
        public string? Note { get; set; }
        public string DateSend { get; set; }
        public string? TrackingNumber { get; set; }
        public RequestShippingStatusEnum Status { get; set; }  
        public ShippingPartnerDTO ShippingPartner { get; set; }
    }
}
