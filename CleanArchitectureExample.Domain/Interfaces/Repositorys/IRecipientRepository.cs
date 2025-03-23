using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces.Repositorys
{
    public interface IRecipientRepository
    {
        Task<Recipient> GetByIdAsync(int id);
        Task<IEnumerable<Recipient>> GetAllAsync();
        Task AddAsync(Recipient recipient);
        Task UpdateAsync(Recipient recipient);
        Task DeleteAsync(Recipient recipient);
        Task<List<Recipient>> GetByConditionAsync(Expression<Func<Recipient, bool>> expression);

        
    }
}
