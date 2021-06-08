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

        public async Task<IEnumerable<MaterialInventoryTransaction>> ListAsync(int materialId)
        {
            return await (from u in _context.MaterialInventoryTransaction
                          orderby u.CreatedUtc descending
                          where u.MaterialId == materialId
                            && !u.DeletedUtc.HasValue
                          select u)
                        .ToListAsync();
        }

        public async Task<MaterialInventoryTransaction> GetAsync(int id)
        {
            return await (from u in _context.MaterialInventoryTransaction
                          where u.Id == id
                            && !u.DeletedUtc.HasValue
                          select u)
                        .FirstOrDefaultAsync();
        }
    }
}
