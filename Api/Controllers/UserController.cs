using Api.Models.Dto;
using Api.Services;
using Api.Validation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("/user")]
    [ApiController]
    public class UserController : InventoryControllerBase
    {
        private readonly UserPostValidator _userPostValidator;
        private readonly UserRequestService _userRequestService;

        public UserController(
            AuthenticationDetailService authDetailService,
            UserRequestService userRequestService,
            UserPostValidator userPostValidator) : base(authDetailService)
        {
            _userRequestService = userRequestService;
            _userPostValidator = userPostValidator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            var result = await _userRequestService.ProcessGetRequestAsync(id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(UserModel model)
        {
            if (!await _userPostValidator.IsValidAsync(model))
                return _userPostValidator.ServiceResponse.ToActionResult();

            var userId = await GetCurrentUserIdAsync(User);

            var result = await _userRequestService.ProcessCreateRequestAsync(model, userId);
            return result.ToActionResult(
                Url.Action("Get", "User", new { id = result.Data?.Id }));
        }
    }
}
