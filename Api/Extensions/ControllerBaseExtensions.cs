using Api.Enums;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Api.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static ActionResult GetResultFromServiceResponse(this ControllerBase controller, ServiceResponse response)
        {
            ActionResult result;

            // Handle the status result
            var responseStatus = response.GetStatus();
            switch (responseStatus)
            {
                case ServiceResponseStatus.Success:
                    result = controller.NoContent();
                    break;
                case ServiceResponseStatus.NotFound:
                    result = controller.NotFound(response.GetErrorJson());
                    break;
                case ServiceResponseStatus.Error:
                    result = controller.BadRequest(response.GetErrorJson());
                    break;
                case ServiceResponseStatus.Exception:
                    result = controller.StatusCode(StatusCodes.Status500InternalServerError, response.GetErrorJson());
                    break;
                default:
                    result = controller.StatusCode(StatusCodes.Status500InternalServerError);
                    break;
            }

            return result;
        }

        public static ActionResult GetResultFromServiceResponse<T>(this ControllerBase controller, ServiceResponse<T> response, string uri = null)
        {
            ActionResult result;

            // Handle the generic result
            var responseStatus = response.GetStatus();
            if (responseStatus == ServiceResponseStatus.Success)
            {
                if (response.Data != null)
                {
                    if (!string.IsNullOrEmpty(uri))
                    {
                        result = controller.Created(uri, response.Data);
                    }
                    else
                    {
                        result = controller.Ok(response.Data);
                    }
                }
                else
                {
                    result = controller.NoContent();
                }
            }
            else
            {
                result = controller.GetResultFromServiceResponse(response.ToNonGeneric());
            }

            return result;
        }

        public static int GetCurrentUserId(this ControllerBase controller, ClaimsPrincipal principal)
        {
            var userId = -1;

            _ = int.TryParse(principal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault(), out userId);
            
            return userId;
        }
    }
}
