using Api.Extensions;
using Api.Models;
using Api.Services;
using Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private const CategoryType CATEGORY_TYPE = CategoryType.Product;

        private readonly CategoryRequestService _categoryRequestService;

        public ProductCategoryController(
            CategoryRequestService categoryRequestService)
        {
            _categoryRequestService = categoryRequestService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> Get()
        {
            // Get data from service
            var result = await _categoryRequestService.ProcessListRequestAsync(CATEGORY_TYPE);
            return this.GetResultFromServiceResponse(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> Get(int id)
        {
            // Get data from service
            var result = await _categoryRequestService.ProcessGetRequestAsync(id);
            return this.GetResultFromServiceResponse(result);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<CategoryModel>> Post(CategoryModel model)
        {
            // Get current user id
            var userId = this.GetCurrentUserId(User);

            // Create new record
            var result = await _categoryRequestService.ProcessCreateRequestAsync(model, CATEGORY_TYPE, userId);
            return this.GetResultFromServiceResponse(result,
                Url.Action("Get", "ProductCategory", new { id = result.Data?.Id }));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CategoryModel model)
        {
            // Get current user id
            var userId = this.GetCurrentUserId(User);

            // Update existing record
            var result = await _categoryRequestService.ProcessUpdateRequestAsync(id, model, userId);
            return this.GetResultFromServiceResponse(result);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Get current user id
            var userId = this.GetCurrentUserId(User);

            // Update existing record
            var result = await _categoryRequestService.ProcessDeleteRequestAsync(id, userId);
            return this.GetResultFromServiceResponse(result);
        }
    }
}
