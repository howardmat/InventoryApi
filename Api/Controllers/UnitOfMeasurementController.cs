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
    public class UnitOfMeasurementController : ControllerBase
    {
        private readonly UnitOfMeasurementRequestService _unitOfMeasurementService;

        public UnitOfMeasurementController(
            UnitOfMeasurementRequestService unitOfMeasurementService)
        {
            _unitOfMeasurementService = unitOfMeasurementService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitOfMeasurementModel>>> Get()
        {
            // Get data from service
            var result = await _unitOfMeasurementService.ProcessListRequestAsync();
            return this.GetResultFromServiceResponse(result);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitOfMeasurementModel>> Get(int id)
        {
            // Get data from service
            var result = await _unitOfMeasurementService.ProcessGetRequestAsync(id);
            return this.GetResultFromServiceResponse(result);
        }
    }
}
