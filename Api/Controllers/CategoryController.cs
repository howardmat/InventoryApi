using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Services;
using Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers;

[Authorize]
[Route("/category")]
[ApiController]
public class CategoryController : InventoryControllerBase
{
    private readonly CategoryEntityService _categoryEntityService;

    public CategoryController(
        AuthenticationDetailService authDetailService,
        CategoryEntityService categoryEntityService) : base(authDetailService)
    {
        _categoryEntityService = categoryEntityService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryModel>>> Get(CategoryType type)
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _categoryEntityService.ListAsync(type, user.TenantId.Value);
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryModel>> Get(int id)
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _categoryEntityService.GetModelOrDefaultAsync(id, user.TenantId.Value);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<CategoryModel>> Post(CategoryRequest model)
    {
        //todo Add validation for CategoryType
        var type = (CategoryType)Enum.Parse(typeof(CategoryType), model.Type);

        var user = await GetCurrentUserAsync(User);

        var result = await _categoryEntityService.CreateAsync(model.Name, type, user);
        return result.ToActionResult(
            Url.Action("Get", "Category", new { id = result.Data?.Id }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CategoryRequest model)
    {
        //todo Add validation for CategoryType
        var type = (CategoryType)Enum.Parse(typeof(CategoryType), model.Type);

        var user = await GetCurrentUserAsync(User);

        var result = await _categoryEntityService.UpdateAsync(id, model.Name, type, user);
        return result.ToActionResult();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await GetCurrentUserAsync(User);

        var result = await _categoryEntityService.DeleteAsync(id, user);
        return result.ToActionResult();
    }
}