using Api.Handlers;
using Api.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ProvinceRequestService
    {
        private readonly ProvinceEntityService _provinceEntityService;

        public ProvinceRequestService(
            ProvinceEntityService provinceEntityService)
        {
            _provinceEntityService = provinceEntityService;
        }

        public async Task<ResponseHandler<IEnumerable<ProvinceModel>>> ProcessListRequestAsync(string countryCode)
        {
            var response = new ResponseHandler<IEnumerable<ProvinceModel>>();

            response.Data = await _provinceEntityService.ListAsync(countryCode);

            return response;
        }

        public async Task<ResponseHandler<ProvinceModel>> ProcessGetRequestAsync(string countryCode, string provinceCode)
        {
            var response = new ResponseHandler<ProvinceModel>();

            response.Data = await _provinceEntityService.GetAsync(countryCode, provinceCode);

            return response;
        }
    }
}
