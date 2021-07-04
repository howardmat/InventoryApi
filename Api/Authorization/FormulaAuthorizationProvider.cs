using Data;
using System.Threading.Tasks;

namespace Api.Authorization
{
    public class FormulaAuthorizationProvider : IResourceAuthorizationProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        public FormulaAuthorizationProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> TenantHasAccessAsync(int tenantId, int entityId)
        {
            return (await _unitOfWork.FormulaRepository.GetAsync(entityId, tenantId)) != null;
        }
    }
}
