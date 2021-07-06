using Data.Extensions;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    class ProductInventoryTransactionRepository : Repository<ProductInventoryTransaction>, IProductInventoryTransactionRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;

        public ProductInventoryTransactionRepository(InventoryDbContext context) : base(context) { }

        public async Task<IEnumerable<ProductInventoryTransaction>> ListAsync(int productId, int tenantId)
        {
            return await _context.ProductInventoryTransaction
                .WhereNotDeleted()
                .WhereBelongsToTenant(tenantId)
                .Where(m => m.ProductId == productId)
                .ToListAsync();
        }

        public async Task<ProductInventoryTransaction> GetAsync(int id, int tenantId)
        {
            return await _context.ProductInventoryTransaction
                .WhereNotDeleted()
                .WhereBelongsToTenant(tenantId)
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
