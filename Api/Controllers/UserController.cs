using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : InventoryControllerBase
    {
        private readonly UserRequestService _userRequestService;

        public UserController(
            UserRequestService userRequestService)
        {
            _userRequestService = userRequestService;
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
            // Get current user id
            var userId = await GetCurrentUserIdAsync(User);

            // Create new record
            var result = await _userRequestService.ProcessCreateRequestAsync(model, userId);
            return GetResultFromServiceResponse(result,
                Url.Action("Get", "User", new { id = result.Data?.Id }));
        }
    }
}
