using Api.Claims;
using Api.Enums;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class InventoryControllerBase : ControllerBase
    {
        private readonly UserQueryService _userQueryService;

        public InventoryControllerBase() { }

        public InventoryControllerBase(
            UserQueryService userQueryService)
        {
            _userQueryService = userQueryService;
        }

        protected ActionResult GetResultFromServiceResponse(ServiceResponse response)
        {
            ActionResult result;

            // Handle the status result
            var responseStatus = response.GetStatus();
            switch (responseStatus)
            {
                case ServiceResponseStatus.Success:
                    result = NoContent();
                    break;
                case ServiceResponseStatus.NotFound:
                    result = NotFound(response.GetErrorJson());
                    break;
                case ServiceResponseStatus.Error:
                    result = BadRequest(response.GetErrorJson());
                    break;
                case ServiceResponseStatus.Exception:
                    result = StatusCode(StatusCodes.Status500InternalServerError, response.GetErrorJson());
                    break;
                default:
                    result = StatusCode(StatusCodes.Status500InternalServerError);
                    break;
            }

            return result;
        }

        protected ActionResult<T> GetResultFromServiceResponse<T>(ServiceResponse<T> response, string uri = null)
        {
            ActionResult<T> result;

            // Handle the generic result
            var responseStatus = response.GetStatus();
            if (responseStatus == ServiceResponseStatus.Success)
            {
                if (response.Data != null)
                {
                    if (!string.IsNullOrEmpty(uri))
                    {
                        result = Created(uri, response.Data);
                    }
                    else
                    {
                        result = Ok(response.Data);
                    }
                }
                else
                {
                    result = NoContent();
                }
            }
            else
            {
                result = GetResultFromServiceResponse(response.ToNonGeneric());
            }

            return result;
        }

        protected async Task<int> GetCurrentUserIdAsync(ClaimsPrincipal principal)
        {
            if (_userQueryService == null) throw new Exception("UserQueryService is required for this method to be called.");

            var authProviderUserId = principal.Claims.Where(c => c.Type == CustomClaimTypes.UserId).Select(c => c.Value).FirstOrDefault();

            var userId = await _userQueryService.GetUserIdByAuthProviderIdAsync(authProviderUserId);

            return userId;
        }
    }
}
