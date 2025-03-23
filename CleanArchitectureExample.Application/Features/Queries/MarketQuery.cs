using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Application.DTOs;
using MediatR;

namespace CleanArchitectureExample.Application.Features.Queries
{
    public class MarketQuery
    {
        public class GetListMarket : IRequest<List<MarketDTO>>
        {

        }
        public class GetMarketById : IRequest<MarketDTO>
        {
            public int Id { get; set; }
            public GetMarketById(int id)
            {
                Id = id;
            }
        }
    }
}
