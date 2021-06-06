using Api.Models;
using Api.Models.Dto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ProvinceRequestService
    {
        private readonly ILogger<ProvinceRequestService> _logger;
        private readonly ProvinceEntityService _provinceEntityService;

        public ProvinceRequestService(
            ILogger<ProvinceRequestService> logger,
            ProvinceEntityService provinceEntityService)
        {
            _logger = logger;
            _provinceEntityService = provinceEntityService;
        }

        public async Task<ServiceResponse<IEnumerable<ProvinceModel>>> ProcessListRequestAsync(string countryCode)
        {
            var response = new ServiceResponse<IEnumerable<ProvinceModel>>();

            try
            {
                response.Data = await _provinceEntityService.ListAsync(countryCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("ProvinceRequestService.ProcessListRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<ProvinceModel>> ProcessGetRequestAsync(string provinceCode)
        {
            var response = new ServiceResponse<ProvinceModel>();

            try
            {
                response.Data = await _provinceEntityService.GetAsync(provinceCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("ProvinceRequestService.ProcessGetRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
