using Data.Repositories;
using System.Threading.Tasks;

namespace Data
{
    public class InventoryUnitOfWork : IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public ICountryRepository CountryRepository { get; }
        public IFormulaRepository FormulaRepository { get; }
        public IFormulaIngredientRepository FormulaIngredientRepository { get; }
        public IMaterialRepository MaterialRepository { get; }
        public IMaterialInventoryTransactionRepository MaterialInventoryTransactionRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IProvinceRepository ProvinceRepository { get; }
        public ITenantRepository TenantRepository { get; }
        public IUnitOfMeasurementRepository UnitOfMeasurementRepository { get; }
        public IUserRepository UserRepository { get; }

        private readonly InventoryDbContext _context;

        public InventoryUnitOfWork(InventoryDbContext context)
        {
            _context = context;

            CategoryRepository = new CategoryRepository(_context);
            CountryRepository = new CountryRepository(_context);
            FormulaRepository = new FormulaRepository(_context);
            FormulaIngredientRepository = new FormulaIngredientRepository(_context);
            MaterialRepository = new MaterialRepository(_context);
            MaterialInventoryTransactionRepository = new MaterialInventoryTransactionRepository(_context);
            ProductRepository = new ProductRepository(_context);
            ProvinceRepository = new ProvinceRepository(_context);
            TenantRepository = new TenantRepository(_context);
            UnitOfMeasurementRepository = new UnitOfMeasurementRepository(_context);
            UserRepository = new UserRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
