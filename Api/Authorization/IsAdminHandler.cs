using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Api.Authorization
{
    public class IsAdminHandler : AuthorizationHandler<IsAdminAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IsAdminAuthorizationRequirement requirement)
        {
            return Task.CompletedTask;
        }
    }
}
