using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IUnitOfMeasurementRepository : IRepository<UnitOfMeasurement>
    {
        Task<IEnumerable<UnitOfMeasurement>> ListAsync();
        Task<UnitOfMeasurement> GetAsync(int id);
    }
}
