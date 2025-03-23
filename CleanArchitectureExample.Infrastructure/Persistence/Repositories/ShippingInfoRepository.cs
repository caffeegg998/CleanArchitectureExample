using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces.Repositorys;
using CleanArchitectureExample.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Infrastructure.Persistence.Repositories
{
    public class ShippingInfoRepository : IShippingInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public ShippingInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ShippingInfo shippingInfo)
        {
            await _context.ShippingInfos.AddAsync(shippingInfo);
        }

        public async Task DeleteAsync(ShippingInfo shippingInfo)
        {
            _context.ShippingInfos.Remove(shippingInfo);
        }

        public async Task<IEnumerable<ShippingInfo>> GetAllAsync()
        {
            return await _context.ShippingInfos.ToListAsync();
        }

        public async Task<ShippingInfo> GetByIdAsync(int id)
        {
            return await _context.ShippingInfos.FindAsync(id);
        }

        public async Task<IEnumerable<ShippingInfo>> GetByConditionAsync(Expression<Func<ShippingInfo, bool>> expression)
        {
            return await _context.ShippingInfos.Where(expression).ToListAsync();
        }

        public async Task UpdateAsync(ShippingInfo shippingInfo)
        {
            _context.ShippingInfos.Update(shippingInfo);
        }
    }
}
