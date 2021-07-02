using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IMaterialInventoryTransactionRepository : IRepository<MaterialInventoryTransaction>
    {
        Task<IEnumerable<MaterialInventoryTransaction>> ListAsync(int materialId, int tenantId);
        Task<MaterialInventoryTransaction> GetAsync(int id, int tenantId);
    }
}
