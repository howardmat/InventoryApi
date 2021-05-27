using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;
        public UserRepository(InventoryDbContext context) : base(context) { }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await (from u in _context.User
                          orderby u.Email
                          where !u.DeletedUtc.HasValue
                          select u)
                        .ToListAsync();
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await (from u in _context.User
                          where u.Email.ToLower() == email.ToLower()
                          select u)
                        .FirstOrDefaultAsync();
        }

        public async Task<User> FindByLocalIdAsync(string localId)
        {
            return await (from u in _context.User
                          where u.LocalId.ToLower() == localId.ToLower()
                          select u)
                        .FirstOrDefaultAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await (from u in _context.User
                          where u.Id == id
                          select u)
                        .FirstOrDefaultAsync();
        }
    }
}
