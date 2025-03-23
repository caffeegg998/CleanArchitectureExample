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
            public int Id { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
        }
        public class CreateProductCommand : IRequest<int>
        {
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public double Price { get; set; }
            public List<int> MarketIds { get; set; } = new List<int>(); // Danh sách Market liên kết
        }
        public class DeleteProductCommand : IRequest<bool>
        {
            public DeleteProductCommand(int id)
            {
                Id = id;
            }

            public int Id { get; set; }
        }
    }
}
