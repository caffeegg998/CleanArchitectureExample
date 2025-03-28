﻿using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string CreateAt { get; set; }
        public double Price { get; set; }
        public virtual List<MarketSumaryDTO> MarketSumaryDTOs { get; set; }
    }
    public class MarketSumaryDTO
    {
        public int MarketId { get; set; }
        public string MarketName { get; set; }
    }


}
