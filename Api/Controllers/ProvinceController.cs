using Api.Models.Dto;
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

        [HttpGet("/country/{countryIsoCode}/province")]
        public async Task<ActionResult<IEnumerable<ProvinceModel>>> GetAllByCountry(string countryIsoCode)
        {
            var result = await _provinceService.ProcessListRequestAsync(countryIsoCode);
            return result.ToActionResult();
        }

        [HttpGet("/country/{countryIsoCode}/province/{isoCode}")]
        public async Task<ActionResult<ProvinceModel>> GetByCode(string countryIsoCode, string isoCode)
        {
            var result = await _provinceService.ProcessGetRequestAsync(isoCode, countryIsoCode);
            return result.ToActionResult();
        }
    }
}
