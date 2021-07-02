using Data.Extensions;
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
            return await _context.UnitOfMeasurement
                .WhereNotDeleted()
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<UnitOfMeasurement> GetAsync(int id)
        {
            return await _context.UnitOfMeasurement
                .WhereNotDeleted()
                .Where(u => u.Id == id)
                .OrderBy(m => m.Name)
                .FirstOrDefaultAsync();
        }
    }
}
