using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IMaterialRepository : IRepository<Material>
    {
        Task<IEnumerable<Material>> ListAsync(int tenantId);
        Task<Material> GetAsync(int id, int tenantId);
    }
}
