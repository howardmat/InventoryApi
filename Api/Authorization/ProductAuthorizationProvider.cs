using Data;
using System.Threading.Tasks;

namespace Api.Authorization
{
    public class ProductAuthorizationProvider : IResourceAuthorizationProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductAuthorizationProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> TenantHasAccessAsync(int tenantId, int entityId)
        {
            return (await _unitOfWork.ProductRepository.GetAsync(entityId, tenantId)) != null;
        }
    }
}
