using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Domain.Entities;

using CleanArchitectureExample.Infrastructure.Persistence.DbContexts;
using CleanArchitectureExample.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureExample.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(Guid id) => await _context.Products.FindAsync(id);

        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();

        public async Task AddAsync(Product product) => await _context.Products.AddAsync(product);

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }

            // Cập nhật các giá trị từ product vào existingProduct
            existingProduct.ProductName = product.ProductName;
            existingProduct.Price = product.Price;

            // Đánh dấu entity là đã thay đổi
            _context.Products.Update(existingProduct);

            // Đợi quá trình lưu vào DB
            //await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            if (product != null)
            {
                 _context.Products.Remove(product);
            }
        }
    }
}
