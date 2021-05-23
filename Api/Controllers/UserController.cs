using Api.Extensions;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(
            UserService userService)
        {
            _userService = userService;
        }

        // GET api/<controller>/id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            // Get data from service
            var result = await _userService.GetAsync(id);
            return this.GetResultFromServiceResponse(result);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<UserModel>> Post()
        {
            // Get current user id
            //var model = this.GetModelFromClaimsPrincipal(User);

            // Create new record
            //var result = await _userService.CreateOrUpdateAsync(model, model.B2cAccountId);
            //return this.GetResultFromServiceResponse(result,
            //    Url.Action("Get", "User", new { id = result.Data?.B2cAccountId }));

            return this.GetResultFromServiceResponse(null);
        }
    }
}
