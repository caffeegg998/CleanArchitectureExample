using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces.Repositorys
{
    public interface IMarketRepositories
    {
        Task<Market> GetByIdAsync(int id);
        Task<IEnumerable<Market>> GetAllAsync();
        Task AddAsync(Market market);
        Task UpdateAsync(Market market);
        Task DeleteAsync(Market market);
        Task<IEnumerable<Market>> GetByConditionAsync(Expression<Func<Market, bool>> expression);
    }
}
