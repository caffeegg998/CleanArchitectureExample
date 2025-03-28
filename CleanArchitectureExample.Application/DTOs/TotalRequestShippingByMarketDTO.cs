using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.DTOs
{
    public class TotalRequestShippingByMarketDTO
    {
        public string MarketName { get; set; }
        public int TotalRequest { get; set; }
        public TotalRequestStatus TotalRequestStatus { get; set; }
        public List<RequestShippingDTO> ListRequestShipping { get; set; }
    }
    public class TotalRequestStatus
    {
        public int Pending { get; set; } = 0;
        public int Processed { get; set; } = 0;
        public int Shipped { get; set; } = 0;
        public int Delivered { get; set; } = 0;
        public int Cancelled { get; set; } = 0;
        public int Returned { get; set; } = 0;
    }
}
