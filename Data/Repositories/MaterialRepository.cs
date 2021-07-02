using Data.Extensions;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;
        public MaterialRepository(InventoryDbContext context) : base(context) { }

        public async Task<IEnumerable<Material>> ListAsync(int tenantId)
        {
            return await _context.Material
                .WhereNotDeleted()
                .WhereBelongsToTenant(tenantId)
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<Material> GetAsync(int id, int tenantId)
        {
            return await _context.Material
                .WhereNotDeleted()
                .WhereBelongsToTenant(tenantId)
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
