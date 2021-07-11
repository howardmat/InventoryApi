using Data.Extensions;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;

        public ProductRepository(InventoryDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> ListAsync(int tenantId)
        {
            return await _context.Product
                .WhereBelongsToTenant(tenantId)
                .OrderBy(m => m.Name)
                .Include(m => m.Category)
                .Include(m => m.UnitOfMeasurement)
                .ToListAsync();
        }

        public async Task<Product> GetAsync(int id, int tenantId)
        {
            return await _context.Product
                .WhereBelongsToTenant(tenantId)
                .Where(p => p.Id == id)
                .Include(m => m.Category)
                .Include(m => m.UnitOfMeasurement)
                .FirstOrDefaultAsync();
        }
    }
}
