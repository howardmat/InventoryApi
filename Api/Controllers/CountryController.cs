using Api.Extensions;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryRequestService _countryRequestService;

        public CountryController(
            CountryRequestService countryRequestService)
        {
            _countryRequestService = countryRequestService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryModel>>> Get()
        {
            // Get data from service
            var serviceResponse = await _countryRequestService.ProcessListRequestAsync();
            return this.GetResultFromServiceResponse(serviceResponse);
        }
    }
}
