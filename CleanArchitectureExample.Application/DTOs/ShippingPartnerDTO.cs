using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Domain.Entities;

namespace CleanArchitectureExample.Application.DTOs
{
    public class ShippingPartnerDTO
    {
        public int ShippingPartnerId { get; set; }
        public string PartnerName { get; set; }
        public string Region { get; set; }
    }
}
