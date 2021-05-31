using Microsoft.AspNetCore.Builder;

namespace Api.Claims
{
    public static class InjectTenantIdClaimMiddlewareExtensions
    {
        public static IApplicationBuilder UseInjectTenantIdClaim(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InjectTenantIdClaimMiddleware>();
        }
    }
}
