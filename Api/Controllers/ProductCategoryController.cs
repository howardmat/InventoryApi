using Api.Extensions;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Enums;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private const CategoryType CATEGORY_TYPE = CategoryType.Product;

        private readonly CategoryService _categoryService;

        public ProductCategoryController(
            CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> Get()
        {
            // Get data from service
            var result = await _categoryService.ListAsync(CATEGORY_TYPE);
            return this.GetResultFromServiceResponse(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> Get(int id)
        {
            // Get data from service
            var result = await _categoryService.GetAsync(id);
            return this.GetResultFromServiceResponse(result);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<CategoryModel>> Post(CategoryModel model)
        {
            // Get current user id
            var userId = this.GetCurrentUserId(User);

            // Create new record
            var result = await _categoryService.CreateAsync(model, CATEGORY_TYPE, userId);
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
            var result = await _categoryService.UpdateAsync(id, model, userId);
            return this.GetResultFromServiceResponse(result);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Get current user id
            var userId = this.GetCurrentUserId(User);

            // Update existing record
            var result = await _categoryService.DeleteAsync(id, userId);
            return this.GetResultFromServiceResponse(result);
        }
    }
}
