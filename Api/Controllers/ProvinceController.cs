using Api.Models;
using Api.Models.RequestModels;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    public class ProvinceController : InventoryControllerBase
    {
        private readonly ProvinceRequestService _provinceService;

        public ProvinceController(
            ProvinceRequestService provinceService)
        {
            _provinceService = provinceService;
        }

        [HttpGet("api/country/{countryIsoCode}/province")]
        public async Task<ActionResult<IEnumerable<ProvinceModel>>> GetAllByCountry([FromRoute] ProvinceGetAllByCountry model)
        {
            var result = await _provinceService.ProcessListRequestAsync(model.CountryIsoCode);
            return GetResultFromServiceResponse(result);
        }

        [HttpGet("api/country/{countryIsoCode}/province/{isoCode}")]
        public async Task<ActionResult<ProvinceModel>> GetByCode([FromRoute] ProvinceGetByCode model)
        {
            var result = await _provinceService.ProcessGetRequestAsync(model.IsoCode);
            return GetResultFromServiceResponse(result);
        }
    }
}
