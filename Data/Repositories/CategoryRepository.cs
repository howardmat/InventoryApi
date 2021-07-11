using Data.Enums;
using Data.Extensions;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private InventoryDbContext _context => Context as InventoryDbContext;
        public CategoryRepository(InventoryDbContext context) : base(context) { }

        public async Task<IEnumerable<Category>> ListAsync(CategoryType categoryType, int tenantId)
        {
            return await _context.Category
                .WhereBelongsToTenant(tenantId)
                .Where(c => c.Type == categoryType)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Category> GetAsync(int id, int tenantId)
        {
            return await _context.Category
                .WhereBelongsToTenant(tenantId)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Category
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
