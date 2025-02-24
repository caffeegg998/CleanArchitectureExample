using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Features.Commands
{
    public class ProductCommand
    {
        public class UpdateProductCommand : IRequest<bool>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }
        public class CreateProductCommand : IRequest<Guid>
        {
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
        }
        public class DeleteProductCommand : IRequest<bool>
        {
            public DeleteProductCommand(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
        }
    }
}
