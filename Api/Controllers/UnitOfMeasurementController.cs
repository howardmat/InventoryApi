using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/unitofmeasurement")]
    [ApiController]
    public class UnitOfMeasurementController : InventoryControllerBase
    {
        private readonly UnitOfMeasurementRequestService _unitOfMeasurementService;

        public UnitOfMeasurementController(
            UnitOfMeasurementRequestService unitOfMeasurementService)
        {
            _unitOfMeasurementService = unitOfMeasurementService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitOfMeasurementModel>>> Get()
        {
            // Get data from service
            var result = await _unitOfMeasurementService.ProcessListRequestAsync();
            return GetResultFromServiceResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnitOfMeasurementModel>> Get(int id)
        {
            // Get data from service
            var result = await _unitOfMeasurementService.ProcessGetRequestAsync(id);
            return GetResultFromServiceResponse(result);
        }
    }
}
