using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IMaterialInventoryTransactionRepository : IRepository<MaterialInventoryTransaction>
    {
        Task<IEnumerable<MaterialInventoryTransaction>> ListAsync(int materialId);
        Task<MaterialInventoryTransaction> GetAsync(int id);
    }
}
