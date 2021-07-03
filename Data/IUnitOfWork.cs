using Data.Repositories;
using System;
using System.Threading.Tasks;

namespace Data
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        ICountryRepository CountryRepository { get; }
        IFormulaRepository FormulaRepository { get; }
        IFormulaIngredientRepository FormulaIngredientRepository { get; }
        IMaterialRepository MaterialRepository { get; }
        IMaterialInventoryTransactionRepository MaterialInventoryTransactionRepository { get; }
        IProductRepository ProductRepository { get; }
        IProvinceRepository ProvinceRepository { get; }
        ITenantRepository TenantRepository { get; }
        IUnitOfMeasurementRepository UnitOfMeasurementRepository { get; }
        IUserRepository UserRepository { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}
