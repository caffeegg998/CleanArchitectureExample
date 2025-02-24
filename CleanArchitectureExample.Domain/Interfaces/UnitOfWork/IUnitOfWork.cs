using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Domain.Interfaces;
using CleanArchitectureExample.Infrastructure.Persistence.Repositories.Interfaces;

namespace CleanArchitectureExample.Infrastructure.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        Task<int> CompleteAsync();
        Task<int> CompleteIdentityAsync();
    }
}
