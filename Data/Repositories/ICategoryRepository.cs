using Data.Enums;
using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> ListAsync(CategoryType categoryType);
        Task<Category> GetAsync(int id);
    }
}
