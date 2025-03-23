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
    public class MarketRepositories : IMarketRepositories
    {
        private readonly ApplicationDbContext _context;

        public MarketRepositories(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Market market)
        {
            await _context.Markets.AddAsync(market);
        }

        public async Task DeleteAsync(Market market)
        {
            _context.Markets.Remove(market);
        }

        public async Task<IEnumerable<Market>> GetAllAsync()
        {
            return await _context.Markets
                .Include(m => m.Products) // Load danh sách Product
                .ToListAsync();
        }

        public async Task<IEnumerable<Market>> GetByConditionAsync(Expression<Func<Market, bool>> expression)
        {
            return await _context.Markets.Where(expression).ToListAsync();
        }

        public async Task<Market> GetByIdAsync(int id)
        {
            return await _context.Markets
               .Include(m => m.Products) // Load danh sách Product của Market nếu có
               .FirstOrDefaultAsync(m => m.MarketId == id);
        }

        public async Task UpdateAsync(Market market)
        {
            _context.Markets.Update(market);
        }
    }
}
