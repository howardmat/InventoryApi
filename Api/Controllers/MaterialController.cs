using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/material")]
    [ApiController]
    public class MaterialController : InventoryControllerBase
    {
        private readonly MaterialRequestService _materialRequestService;

        public MaterialController(
            MaterialRequestService materialRequestService)
        {
            _materialRequestService = materialRequestService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialModel>>> Get()
        {
            // Get data from service
            var result = await _materialRequestService.ProcessListRequestAsync();
            return GetResultFromServiceResponse(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialModel>> Get(int id)
        {
            // Get data from service
            var result = await _materialRequestService.ProcessGetRequestAsync(id);
            return GetResultFromServiceResponse(result);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<MaterialModel>> Post(MaterialModel model)
        {
            // Get current user id
            var userId = await GetCurrentUserIdAsync(User);

            // Create new record
            var result = await _materialRequestService.ProcessCreateRequestAsync(model, userId);
            return GetResultFromServiceResponse(result,
                Url.Action("Get", "Material", new { id = result.Data?.Id }));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MaterialModel model)
        {
            // Get current user id
            var userId = await GetCurrentUserIdAsync(User);

            // Update existing record
            var result = await _materialRequestService.ProcessUpdateRequestAsync(id, model, userId);
            return GetResultFromServiceResponse(result);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Get current user id
            var userId = await GetCurrentUserIdAsync(User);

            // Update existing record
            var result = await _materialRequestService.ProcessDeleteRequestAsync(id, userId);
            return GetResultFromServiceResponse(result);
        }
    }
}
