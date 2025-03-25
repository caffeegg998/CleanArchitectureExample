using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Enums;
using CleanArchitectureExample.Domain.Interfaces.Repositorys;
using CleanArchitectureExample.Domain.Utils;
using CleanArchitectureExample.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Infrastructure.Persistence.Repositories
{
    public class RequestShippingRepositories : IRequestShippingRepositories
    {
        private readonly ApplicationDbContext _context;
        public RequestShippingRepositories(ApplicationDbContext context) {
            _context = context;
        }
        public async Task AddAsync(RequestShipping requestShipping)
        {
            await _context.RequestShippings.AddAsync(requestShipping);
        }

        public async Task DeleteAsync(RequestShipping requestShipping)
        {
            _context.RequestShippings.Remove(requestShipping);
        }

        public async Task<List<RequestShipping>> GetAllAsync()
        {
            return await _context.RequestShippings
            .Include(r => r.UserProfile)
            .Include(r => r.Recipient)
            .Include(r => r.Product)
            .Include(r => r.ShippingInfo)
            .ToListAsync();
        }

        public async Task<RequestShipping?> GetByIdAsync(int id)
        {
            return await _context.RequestShippings
            .Include(r => r.UserProfile)
            .Include(r => r.Recipient)
            .Include(r => r.Product)
            .Include(r => r.ShippingInfo)
            .FirstOrDefaultAsync(r => r.RequestShippingId == id);
        }

        public async Task<List<RequestShipping>> GetByMarketIdAsync(int marketId)
        {
            return await _context.RequestShippings
                .Include(rs => rs.Product)
                .Include(rs => rs.Recipient)
                .Include(rs => rs.Page)
                .Include(rs => rs.UserProfile)
                .Include(rs => rs.ShippingInfo)          // Load ShippingInfo
                .ThenInclude(si => si.ShippingPartner)   // Load ShippingPartner
                .ThenInclude(sp => sp.Market)           // Load Market
                .Where(rs => rs.ShippingInfo.ShippingPartner.MarketId == marketId)
                .ToListAsync();
        }

        public async Task<RequestShipping> UpdateAsync(RequestShipping requestShipping)
        {
            requestShipping.Status = RequestShippingStatusEnum.Processed;
            ShippingInfo shippingInfo = await _context.ShippingInfos.FindAsync(requestShipping.ShippingInfoId);
            if(shippingInfo != null)
            {
                shippingInfo.DateSend = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                shippingInfo.Status = RequestShippingStatusEnum.Processed;
                shippingInfo.TrackingNumber = ShippingInfoGenerator.GenerateTrackingNumber();

                _context.ShippingInfos.Update(shippingInfo);
            }
            _context.RequestShippings.Update(requestShipping);
            return requestShipping;
        }
    }
}
