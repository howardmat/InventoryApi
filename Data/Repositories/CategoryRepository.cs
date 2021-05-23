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

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await (from c in _context.Category
                          orderby c.Name
                          where !c.DeletedUtc.HasValue
                          select c)
                        .ToListAsync();
        }

        public async Task<Category> GetAsync(int id)
        {
            return await (from c in _context.Category
                          where c.Id == id
                            && !c.DeletedUtc.HasValue
                          select c)
                        .FirstOrDefaultAsync();
        }
    }
}
