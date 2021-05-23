using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;
        public UserRepository(InventoryDbContext context) : base(context) { }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await (from u in _context.User
                          where u.Email.ToLower() == email.ToLower()
                          select u)
                        .FirstOrDefaultAsync();
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await (from u in _context.User
                          where u.Id == id
                          select u)
                        .FirstOrDefaultAsync();
        }
    }
}
