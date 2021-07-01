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

        public async Task<IEnumerable<Material>> ListAsync()
        {
            return await (from m in _context.Material
                          orderby m.Name
                          where !m.DeletedUtc.HasValue
                          select m)
                        .ToListAsync();
        }

        public async Task<Material> GetAsync(int id)
        {
            return await (from m in _context.Material
                          where m.Id == id
                            && !m.DeletedUtc.HasValue
                          select m)
                        .FirstOrDefaultAsync();
        }
    }
}
