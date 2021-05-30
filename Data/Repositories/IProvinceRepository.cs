using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IProvinceRepository : IRepository<Province>
    {
        Task<Province> GetAsync(string provinceCode);
        Task<IEnumerable<Province>> ListAsync(string countryCode);
    }
}
