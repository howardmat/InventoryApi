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
            return await (from p in _context.Province
                          where p.CountryIsoCode.ToLower() == countryCode.ToLower()
                            && p.IsoCode.ToLower() == provinceCode.ToLower()
                          select p)
                          .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Province>> ListAsync(string countryCode)
        {
            return await (from p in _context.Province
                          where p.CountryIsoCode.ToLower() == countryCode.ToLower()
                          orderby p.DisplayOrder, p.Name
                          select p)
                        .ToListAsync();
        }
    }
}
