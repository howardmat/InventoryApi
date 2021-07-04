using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Services;
using Api.Validation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize(Policy = "IsAdmin")]
    [Route("/user")]
    [ApiController]
    public class UserController : InventoryControllerBase
    {
        private readonly UserRequestValidator _userRequestValidator;
        private readonly UserRequestService _userRequestService;

        public UserController(
            AuthenticationDetailService authDetailService,
            UserRequestService userRequestService,
            UserRequestValidator userRequestValidator) : base(authDetailService)
        {
            _userRequestService = userRequestService;
            _userRequestValidator = userRequestValidator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            var result = await _userRequestService.ProcessGetRequestAsync(id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(UserRequest model)
        {
            if (!await _userRequestValidator.IsValidAsync(model))
                return _userRequestValidator.ServiceResponse.ToActionResult();

            var userId = await GetCurrentUserIdAsync(User);

            var result = await _userRequestService.ProcessCreateRequestAsync(model, userId);
            return result.ToActionResult(
                Url.Action("Get", "User", new { id = result.Data?.Id }));
        }
    }
}
