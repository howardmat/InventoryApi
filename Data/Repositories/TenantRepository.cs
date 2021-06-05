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
            return await (from t in _context.Tenant
                          orderby t.CompanyName
                          where !t.DeletedUtc.HasValue
                          select t)
                        .ToListAsync();
        }

        public async Task<Tenant> GetAsync(int id)
        {
            return await (from e in _context.Tenant
                          where e.Id == id
                            && !e.DeletedUtc.HasValue
                          select e)
                          .Include(t => t.PrimaryAddress.Country)
                          .Include(t => t.PrimaryAddress.Province)
                        .FirstOrDefaultAsync();
        }

        public async Task<Tenant> FindByOwnerIdAsync(int ownerUserId)
        {
            return await (from e in _context.Tenant
                          where e.OwnerUserId == ownerUserId
                            && !e.DeletedUtc.HasValue
                          select e)
                        .FirstOrDefaultAsync();
        }
    }
}
