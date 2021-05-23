using Data.Models;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByIdAsync(int id);
    }
}
