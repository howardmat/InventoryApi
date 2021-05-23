using Api.Extensions;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly MaterialService _materialService;

        public MaterialController(
            MaterialService materialService)
        {
            _materialService = materialService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialModel>>> Get()
        {
            // Get data from service
            var result = await _materialService.ListAsync();
            return this.GetResultFromServiceResponse(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialModel>> Get(int id)
        {
            // Get data from service
            var result = await _materialService.GetAsync(id);
            return this.GetResultFromServiceResponse(result);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<MaterialModel>> Post(MaterialModel model)
        {
            // Get current user id
            var userId = this.GetCurrentUserId(User);

            // Create new record
            var result = await _materialService.CreateAsync(model, userId);
            return this.GetResultFromServiceResponse(result,
                Url.Action("Get", "Material", new { id = result.Data?.Id }));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MaterialModel model)
        {
            // Get current user id
            var userId = this.GetCurrentUserId(User);

            // Update existing record
            var result = await _materialService.UpdateAsync(id, model, userId);
            return this.GetResultFromServiceResponse(result);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Get current user id
            var userId = this.GetCurrentUserId(User);

            // Update existing record
            var result = await _materialService.DeleteAsync(id, userId);
            return this.GetResultFromServiceResponse(result);
        }
    }
}
