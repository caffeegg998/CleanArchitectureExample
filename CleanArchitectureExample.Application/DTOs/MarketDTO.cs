using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.DTOs
{
    public class MarketDTO
    {
        public int MarketId { get; set; }
        public string MarketName { get; set; }

        public List<ProductSummaryDTO> Products { get; set; }
    }
}
