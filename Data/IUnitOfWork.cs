using Data.Repositories;
using System;
using System.Threading.Tasks;

namespace Data
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        ICountryRepository CountryRepository { get; }
        IMaterialRepository MaterialRepository { get; }
        IProvinceRepository ProvinceRepository { get; }
        ITenantRepository TenantRepository { get; }
        IUnitOfMeasurementRepository UnitOfMeasurementRepository { get; }
        IUserRepository UserRepository { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}
