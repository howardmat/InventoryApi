using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;
        public CountryRepository(InventoryDbContext context) : base(context) { }

        public async Task<IEnumerable<Country>> ListAsync()
        {
            return await (from c in _context.Country
                          where !c.DeletedUtc.HasValue
                          orderby c.DisplayOrder, c.Name
                          select c)
                        .ToListAsync();
        }
    }
}
