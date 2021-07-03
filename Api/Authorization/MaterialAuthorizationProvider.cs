using Data;
using System.Threading.Tasks;

namespace Api.Authorization
{
    public class MaterialAuthorizationProvider : IResourceAuthorizationProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        public MaterialAuthorizationProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> TenantHasAccessAsync(int tenantId, int entityId)
        {
            return (await _unitOfWork.MaterialRepository.GetAsync(entityId, tenantId)) != null;
        }
    }
}
