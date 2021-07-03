using System.Threading.Tasks;

namespace Api.Authorization
{
    public class ResourceAuthorization<TProvider> where TProvider : IResourceAuthorizationProvider
    {
        private readonly TProvider _provider;

        public ResourceAuthorization(TProvider provider)
        {
            _provider = provider;
        }

        public async Task<bool> TenantHasResourceAccessAsync(int tenantId, int entityId)
        {
            return await _provider.TenantHasAccessAsync(tenantId, entityId);
        }
    }
}
