﻿using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces.Repositorys
{
    public interface IRequestShippingRepositories
    {
        Task<RequestShipping?> GetByIdAsync(int id);
        Task<List<RequestShipping>> GetAllAsync();
        Task AddAsync(RequestShipping requestShipping);
        Task<RequestShipping> UpdateAsync(RequestShipping requestShipping);

        Task DeleteAsync(RequestShipping requestShipping);

        Task<List<RequestShipping>> GetByMarketIdAsync(int marketId);
    }
}
