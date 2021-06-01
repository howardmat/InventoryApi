using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : Repository<UserProfile>, IUserRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;
        public UserRepository(InventoryDbContext context) : base(context) { }

        public async Task<IEnumerable<UserProfile>> ListAsync()
        {
            return await (from u in _context.UserProfile
                          orderby u.Email
                          where !u.DeletedUtc.HasValue
                          select u)
                        .ToListAsync();
        }

        public async Task<UserProfile> FindByEmailAsync(string email)
        {
            return await (from u in _context.UserProfile
                          where u.Email.ToLower() == email.ToLower()
                          select u)
                        .FirstOrDefaultAsync();
        }

        public async Task<UserProfile> FindByLocalIdAsync(string localId)
        {
            return await (from u in _context.UserProfile
                          where u.LocalId.ToLower() == localId.ToLower()
                          select u)
                        .FirstOrDefaultAsync();
        }

        public async Task<UserProfile> GetAsync(int id)
        {
            return await (from u in _context.UserProfile
                          where u.Id == id
                          select u)
                        .FirstOrDefaultAsync();
        }
    }
}
