using Api.Models;
using Api.Models.RequestModels;
using Api.Services;
using Api.Validation.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : InventoryControllerBase
    {
        private readonly RegisterPostValidator _registerPostValidator;
        private readonly RegisterRequestService _registerRequestService;

        public RegisterController(
            RegisterRequestService registerRequestService,
            RegisterPostValidator registerPostValidator)
        {
            _registerRequestService = registerRequestService;
            _registerPostValidator = registerPostValidator;
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(RegisterPost model)
        {
            // Custom validation on UserModel
            if (!await _registerPostValidator.IsValidAsync(model))
                return GetResultFromServiceResponse(_registerPostValidator.ServiceResponse);

            // Create new record
            var result = await _registerRequestService.ProcessRegisterRequestAsync(model);
            return GetResultFromServiceResponse(result,
                Url.Action("Get", "User", new { id = result.Data?.Id }));
        }
    }
}
