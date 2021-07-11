using Data.Extensions;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TenantRepository : Repository<Tenant>, ITenantRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;
        public TenantRepository(InventoryDbContext context) : base(context) { }

        public async Task<IEnumerable<Tenant>> ListAsync()
        {
            return await _context.Tenant
                .OrderBy(p => p.CompanyName)
                .ToListAsync();
        }

        public async Task<Tenant> GetAsync(int id)
        {
            return await _context.Tenant
                .Where(t => t.Id == id)
                .Include(t => t.PrimaryAddress.Country)
                .Include(t => t.PrimaryAddress.Province)
                .FirstOrDefaultAsync();
        }

        public async Task<Tenant> FindByOwnerIdAsync(int ownerUserId)
        {
            return await _context.Tenant
                .Where(t => t.OwnerUserId == ownerUserId)
                .FirstOrDefaultAsync();
        }
    }
}
