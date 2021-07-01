using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;

        public ProductRepository(InventoryDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await (from u in _context.Product
                          orderby u.Name
                          where !u.DeletedUtc.HasValue
                          select u)
                        .ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            return await (from u in _context.Product
                          where u.Id == id
                            && !u.DeletedUtc.HasValue
                          select u)
                        .FirstOrDefaultAsync();
        }
    }
}
