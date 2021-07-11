using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class FormulaIngredientRepository : Repository<FormulaIngredient>, IFormulaIngredientRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;

        public FormulaIngredientRepository(InventoryDbContext context) : base(context) { }

        public async Task<FormulaIngredient> GetAsync(int id, int tenantId)
        {
            return await _context.FormulaIngredient
                .Where(fi => fi.Id == id)
                .Where(fi => fi.Formula.TenantId == tenantId)
                .Include(fi => fi.Formula)
                .Include(fi => fi.Material)
                .FirstOrDefaultAsync();
        }
    }
}
