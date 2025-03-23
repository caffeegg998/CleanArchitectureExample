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
    public class RecipientRepository : IRecipientRepository
    {
        private readonly ApplicationDbContext _context;

        public RecipientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Recipient recipient)
        {
            await _context.Recipients.AddAsync(recipient);
        }

        public async Task DeleteAsync(Recipient recipient)
        {
            _context.Recipients.Remove(recipient);
        }

        public async Task<IEnumerable<Recipient>> GetAllAsync()
        {
            return await _context.Recipients.ToListAsync();
        }

        public async Task<List<Recipient>> GetByConditionAsync(Expression<Func<Recipient, bool>> expression)
        {
            return await _context.Recipients.Where(expression).ToListAsync();
        }

        public async Task<Recipient> GetByIdAsync(int id)
        {
            return await _context.Recipients.FindAsync(id);
        }

        public async Task UpdateAsync(Recipient recipient)
        {
            _context.Recipients.Update(recipient);
        }
    }
}
