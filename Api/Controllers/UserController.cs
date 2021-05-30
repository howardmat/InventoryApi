using Api.Models;
using Api.Services;
using Api.Validation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : InventoryControllerBase
    {
        private readonly UserPostValidator _userPostValidator;
        private readonly UserRequestService _userRequestService;

        public UserController(
            UserQueryService userQueryService,
            UserRequestService userRequestService,
            UserPostValidator userPostValidator) : base(userQueryService)
        {
            _userRequestService = userRequestService;
            _userPostValidator = userPostValidator;
        }

        // GET api/<controller>/id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            // Get data from service
            var result = await _userRequestService.ProcessGetRequestAsync(id);
            return GetResultFromServiceResponse(result);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(UserModel model)
        {
            // Custom validation on UserModel
            if (!await _userPostValidator.IsValidAsync(model)) 
                return GetResultFromServiceResponse(_userPostValidator.ServiceResponse);

            // Get current user id
            var userId = await GetCurrentUserIdAsync(User);

            // Create new record
            var result = await _userRequestService.ProcessCreateRequestAsync(model, userId);
            return GetResultFromServiceResponse(result,
                Url.Action("Get", "User", new { id = result.Data?.Id }));
        }
    }
}
