using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IFormulaRepository : IRepository<Formula>
    {
        Task<IEnumerable<Formula>> ListAsync();
        Task<Formula> GetAsync(int id);
    }
}
