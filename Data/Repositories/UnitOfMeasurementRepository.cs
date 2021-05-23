using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    class UnitOfMeasurementRepository : Repository<UnitOfMeasurement>, IUnitOfMeasurementRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;

        public UnitOfMeasurementRepository(InventoryDbContext context) : base(context) { }

        public async Task<IEnumerable<UnitOfMeasurement>> ListAsync()
        {
            return await (from u in _context.UnitOfMeasurement
                          orderby u.Name
                          where !u.DeletedUtc.HasValue
                          select u)
                        .ToListAsync();
        }

        public async Task<UnitOfMeasurement> GetAsync(int id)
        {
            return await (from u in _context.UnitOfMeasurement
                          where u.Id == id
                            && !u.DeletedUtc.HasValue
                          select u)
                        .FirstOrDefaultAsync();
        }
    }
}
