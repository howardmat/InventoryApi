using Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class InventoryControllerBase : ControllerBase
    {
        private readonly AuthenticationDetailService _authenticationDetailService;

        public InventoryControllerBase() { }

        public InventoryControllerBase(
            AuthenticationDetailService authenticationDetailService)
        {
            _authenticationDetailService = authenticationDetailService;
        }

        protected async Task<int> GetCurrentUserIdAsync(ClaimsPrincipal principal)
        {
            if (_authenticationDetailService == null) throw new Exception("AuthenticationDetailService is required in base constructor for GetCurrentUserIdAsync to be called in child class.");

            var userId = await _authenticationDetailService.GetUserIdBasedOnClaimsAsync(principal);

            return userId;
        }

        protected int GetCurrentTenantId(ClaimsPrincipal principal)
        {
            if (_authenticationDetailService == null) throw new Exception("AuthenticationDetailService is required in base constructor for GetCurrentTenantIdAsync to be called in child class.");

            var tenantId = _authenticationDetailService.GetTenantIdBasedOnClaims(principal);

            return tenantId;
        }
    }
}
