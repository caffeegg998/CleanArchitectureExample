using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces.Repositorys
{
    public interface IShippingInfoRepository
    {
        Task<ShippingInfo> GetByIdAsync(int id);
        Task<IEnumerable<ShippingInfo>> GetAllAsync();
        Task AddAsync(ShippingInfo shippingInfo);
        Task UpdateAsync(ShippingInfo shippingInfo);
        Task DeleteAsync(ShippingInfo shippingInfo);
        Task<IEnumerable<ShippingInfo>> GetByConditionAsync(Expression<Func<ShippingInfo, bool>> expression);
    }
}
