using Data.Models;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IFormulaIngredientRepository : IRepository<FormulaIngredient>
    {
        Task<FormulaIngredient> GetAsync(int id, int tenantId);
    }
}
