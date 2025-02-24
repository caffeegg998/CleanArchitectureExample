using CleanArchitectureExample.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Features.Queries
{
    public class ProductQuery
    {
        public class GetProductByIdQuery : IRequest<Product?>
        {
            public Guid Id { get; set; }

            public GetProductByIdQuery(Guid id)
            {
                Id = id;
            }
        }
    }
}
