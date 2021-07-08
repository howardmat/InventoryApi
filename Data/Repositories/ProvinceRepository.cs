using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProvinceRepository : Repository<Province>, IProvinceRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;
        public ProvinceRepository(InventoryDbContext context) : base(context) { }

        public async Task<Province> GetAsync(string countryCode, string provinceCode)
        {
            return await _context.Province
                .Where(p => p.CountryIsoCode.ToLower() == countryCode.ToLower())
                .Where(p => p.IsoCode.ToLower() == provinceCode.ToLower())
                .Include(p => p.Country)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Province>> ListAsync(string countryCode)
        {
            return await _context.Province
                .Where(p => p.CountryIsoCode.ToLower() == countryCode.ToLower())
                .OrderBy(p => p.DisplayOrder)
                .ThenBy(p => p.Name)
                .Include(p => p.Country)
                .ToListAsync();
        }
    }
}
