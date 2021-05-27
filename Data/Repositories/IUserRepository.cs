using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> ListAsync();
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByLocalIdAsync(string localId);
        Task<User> GetAsync(int id);
    }
}
