using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IProductInventoryTransactionRepository : IRepository<ProductInventoryTransaction>
    {
        Task<IEnumerable<ProductInventoryTransaction>> ListAsync(int productId, int tenantId);
        Task<ProductInventoryTransaction> GetAsync(int id, int tenantId);
    }
}
