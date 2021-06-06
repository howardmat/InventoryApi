using Data.Models;

namespace Data.Repositories
{
    class MaterialInventoryTransactionRepository : Repository<MaterialInventoryTransaction>, IMaterialInventoryTransactionRepository
    {
        public InventoryDbContext _context => Context as InventoryDbContext;

        public MaterialInventoryTransactionRepository(InventoryDbContext context) : base(context) { }
    }
}
