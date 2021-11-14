using Api.Models.Dto;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers;

[Authorize]
[Route("/unitofmeasurement")]
[ApiController]
public class UnitOfMeasurementController : InventoryControllerBase
{
    private readonly UnitOfMeasurementEntityService _unitOfMeasurementService;

    public UnitOfMeasurementController(
        UnitOfMeasurementEntityService unitOfMeasurementService)
    {
        _unitOfMeasurementService = unitOfMeasurementService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnitOfMeasurementModel>>> Get()
    {
        var result = await _unitOfMeasurementService.ListAsync();
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnitOfMeasurementModel>> Get(int id)
    {
        var result = await _unitOfMeasurementService.GetModelOrDefaultAsync(id);
        return result.ToActionResult();
    }
}
