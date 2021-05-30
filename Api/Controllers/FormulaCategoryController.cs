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
    [Route("api/formula/category")]
    [ApiController]
    public class FormulaCategoryController : InventoryControllerBase
    {
        private const CategoryType CATEGORY_TYPE = CategoryType.Formula;

        private readonly CategoryRequestService _categoryService;

        public FormulaCategoryController(
            UserQueryService userQueryService, 
            CategoryRequestService categoryService) : base (userQueryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> Get()
        {
            // Get data from service
            var result = await _categoryService.ProcessListRequestAsync(CATEGORY_TYPE);
            return GetResultFromServiceResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> Get(int id)
        {
            // Get data from service
            var result = await _categoryService.ProcessGetRequestAsync(id);
            return GetResultFromServiceResponse(result);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryModel>> Post(CategoryModel model)
        {
            // Get current user id
            var userId = await GetCurrentUserIdAsync(User);

            // Create new record
            var result = await _categoryService.ProcessCreateRequestAsync(model, CATEGORY_TYPE, userId);
            return GetResultFromServiceResponse(result,
                Url.Action("Get", "FormulaCategory", new { id = result.Data?.Id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CategoryModel model)
        {
            // Get current user id
            var userId = await GetCurrentUserIdAsync(User);

            // Update existing record
            var result = await _categoryService.ProcessUpdateRequestAsync(id, model, userId);
            return GetResultFromServiceResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Get current user id
            var userId = await GetCurrentUserIdAsync(User);

            // Update existing record
            var result = await _categoryService.ProcessDeleteRequestAsync(id, userId);
            return GetResultFromServiceResponse(result);
        }
    }
}
