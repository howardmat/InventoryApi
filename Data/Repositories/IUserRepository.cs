using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IUserRepository : IRepository<UserProfile>
    {
        Task<IEnumerable<UserProfile>> ListAsync();
        Task<UserProfile> FindByEmailAsync(string email);
        Task<UserProfile> FindByLocalIdAsync(string localId);
        Task<UserProfile> GetAsync(int id);
    }
}
