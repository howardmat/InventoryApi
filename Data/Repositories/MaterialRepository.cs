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
            return await (from u in _context.Material
                          orderby u.Name
                          where !u.DeletedUtc.HasValue
                          select u)
                        .ToListAsync();
        }

        public async Task<Material> GetAsync(int id)
        {
            return await (from u in _context.Material
                          where u.Id == id
                            && !u.DeletedUtc.HasValue
                          select u)
                        .FirstOrDefaultAsync();
        }
    }
}
