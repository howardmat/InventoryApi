using Api.Models.Dto;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers;

[Authorize]
[Route("/country")]
[ApiController]
public class CountryController : InventoryControllerBase
{
    private readonly CountryEntityService _countryEntityService;

    public CountryController(
        CountryEntityService countryEntityService)
    {
        _countryEntityService = countryEntityService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CountryModel>>> Get()
    {
        var result = await _countryEntityService.ListAsync();
        return result.ToActionResult();
    }
}
