using Data.Extensions;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    class MaterialInventoryTransactionRepository : Repository<MaterialInventoryTransaction>, IMaterialInventoryTransactionRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;

        public MaterialInventoryTransactionRepository(InventoryDbContext context) : base(context) { }

        public async Task<IEnumerable<MaterialInventoryTransaction>> ListAsync(int materialId, int tenantId)
        {
            return await _context.MaterialInventoryTransaction
                .WhereNotDeleted()
                .WhereBelongsToTenant(tenantId)
                .Where(m => m.MaterialId == materialId)
                .ToListAsync();
        }

        public async Task<MaterialInventoryTransaction> GetAsync(int id, int tenantId)
        {
            return await _context.MaterialInventoryTransaction
                .WhereNotDeleted()
                .WhereBelongsToTenant(tenantId)
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
