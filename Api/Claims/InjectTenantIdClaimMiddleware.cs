using Api.Services;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Claims
{
    public class InjectTenantIdClaimMiddleware
    {
        private readonly RequestDelegate _next;

        public InjectTenantIdClaimMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext,
            UserEntityService userEntityService,
            AuthenticationDetailService authDetailService)
        {
            if (httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
            {
                var userId = await authDetailService.GetUserIdBasedOnClaimsAsync(httpContext.User);
                var user = await userEntityService.GetEntityOrDefaultAsync(userId);

                var identity = httpContext.User.Identities.FirstOrDefault();
                if (identity != null)
                {
                    identity.AddClaim(new Claim(CustomClaimTypes.TenantId, user.TenantId.ToString()));
                }
            }

            await _next(httpContext);
        }
    }
}
