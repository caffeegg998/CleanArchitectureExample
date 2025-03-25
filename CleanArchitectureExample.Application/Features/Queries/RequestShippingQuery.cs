using CleanArchitectureExample.Application.DTOs;
using CleanArchitectureExample.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Features.Queries
{
    public class RequestShippingQuery
    {
        public class GetListRequestShipping : IRequest<List<RequestShipping>>
        {
            public GetListRequestShipping()
            {
                
            }
        }
        public class GetRequestShippingByMarketQuery : IRequest<TotalRequestShippingByMarketDTO>
        {
            public int MarketId { get; set; }

            public GetRequestShippingByMarketQuery(int marketId)
            {
                MarketId = marketId;
            }
        }
    }
}
