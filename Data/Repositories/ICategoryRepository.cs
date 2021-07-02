using Data.Enums;
using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> ListAsync(CategoryType categoryType, int tenantId);
        Task<Category> GetAsync(int id, int tenantId);
        Task<Category> GetByIdAsync(int id);
    }
}
