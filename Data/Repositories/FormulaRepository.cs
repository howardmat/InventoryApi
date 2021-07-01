using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class FormulaRepository : Repository<Formula>, IFormulaRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;

        public FormulaRepository(InventoryDbContext context) : base(context) { }

        public async Task<IEnumerable<Formula>> ListAsync()
        {
            return await (from f in _context.Formula
                          orderby f.Name
                          where !f.DeletedUtc.HasValue
                          select f)
                        .ToListAsync();
        }

        public async Task<Formula> GetAsync(int id)
        {
            return await (from f in _context.Formula
                          where f.Id == id
                            && !f.DeletedUtc.HasValue
                          select f)
                        .FirstOrDefaultAsync();
        }
    }
}
