using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Application.DTOs;
using CleanArchitectureExample.Domain.Entities;
using MediatR;

namespace CleanArchitectureExample.Application.Features.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDTO?>
    {
        public int Id { get; set; }

        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
