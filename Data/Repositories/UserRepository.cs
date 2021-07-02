using Data.Extensions;
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
            return await _context.UserProfile
                .WhereNotDeleted()
                .OrderBy(u => u.Email)
                .ToListAsync();
        }

        public async Task<UserProfile> FindByEmailAsync(string email)
        {
            return await _context.UserProfile
                .WhereNotDeleted()
                .Where(u => u.Email.ToLower() == email.ToLower())
                .FirstOrDefaultAsync();
        }

        public async Task<UserProfile> FindByLocalIdAsync(string localId)
        {
            return await _context.UserProfile
                .WhereNotDeleted()
                .Where(u => u.LocalId.ToLower() == localId.ToLower())
                .FirstOrDefaultAsync();
        }

        public async Task<UserProfile> GetAsync(int id)
        {
            return await _context.UserProfile
                .WhereNotDeleted()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
