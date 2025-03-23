using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Domain.Interfaces;
using CleanArchitectureExample.Domain.Interfaces.Repositorys;
using CleanArchitectureExample.Infrastructure.Persistence.DbContexts;
using CleanArchitectureExample.Infrastructure.Persistence.Repositories;

using CleanArchitectureExample.Infrastructure.Persistence.Repositories.Interfaces;

namespace CleanArchitectureExample.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        //private readonly IdentityDbContext _identityDbContext;
        public IProductRepository ProductRepository { get; }

        public IUserProfileRepository UserProfileRepository { get; }
        public IDepartmentRepositories DepartmentRepositories { get; }

        public IMarketRepositories MarketRepositories { get; }

        public IRecipientRepository RecipientRepository { get; }

        public IShippingInfoRepository ShippingInfoRepository { get; }

        public IRequestShippingRepositories RequestShippingRepositories { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            //_identityDbContext = identityDbContext;
            ProductRepository = new ProductRepository(_context);
            UserProfileRepository = new UserProfileRepository(_context);
            DepartmentRepositories = new DepartmentRepositories(_context);
            MarketRepositories = new MarketRepositories(_context);
            RecipientRepository = new RecipientRepository(_context);
            ShippingInfoRepository = new ShippingInfoRepository(_context);
            RequestShippingRepositories = new RequestShippingRepositories(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();

        public async Task<int> CompleteIdentityAsync() => await _context.SaveChangesAsync();

    }
}
