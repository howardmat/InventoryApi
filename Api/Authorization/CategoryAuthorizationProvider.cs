using Data;
using System.Threading.Tasks;

namespace Api.Authorization
{
    public class CategoryAuthorizationProvider : IResourceAuthorizationProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryAuthorizationProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> TenantHasAccessAsync(int tenantId, int entityId)
        {
            return (await _unitOfWork.CategoryRepository.GetAsync(entityId, tenantId)) != null;
        }
    }
}
