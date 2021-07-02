using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> ListAsync(int tenantId);
        Task<Product> GetAsync(int id, int tenantId);
    }
}
