using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Services;
using Api.Validation.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("/register/userprofile")]
    [ApiController]
    public class RegisterUserController : InventoryControllerBase
    {
        private readonly RegisterUserRequestValidator _registerPostValidator;
        private readonly RegisterUserRequestService _registerRequestService;

        public RegisterUserController(
            RegisterUserRequestService registerRequestService,
            RegisterUserRequestValidator registerPostValidator)
        {
            _registerRequestService = registerRequestService;
            _registerPostValidator = registerPostValidator;
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(RegisterUserRequest model)
        {
            if (!await _registerPostValidator.IsValidAsync(model))
                return _registerPostValidator.ServiceResponse.ToActionResult();

            var result = await _registerRequestService.ProcessRegisterRequestAsync(model);
            return result.ToActionResult(
                Url.Action("Get", "User", new { id = result.Data?.Id }));
        }
    }
}
