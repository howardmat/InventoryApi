using System.Threading.Tasks;

namespace Api.Authorization
{
    public interface IResourceAuthorizationProvider
    {
        Task<bool> TenantHasAccessAsync(int tenantId, int entityId);
    }
}
