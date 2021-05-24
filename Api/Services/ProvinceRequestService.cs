using Api.Models;
using AutoMapper;
using Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ProvinceRequestService
    {
        private readonly ILogger<ProvinceRequestService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProvinceRequestService(
            ILogger<ProvinceRequestService> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<ProvinceModel>>> ProcessListRequestAsync(int countryId)
        {
            var response = new ServiceResponse<IEnumerable<ProvinceModel>>();

            try
            {
                // Fetch data
                var data = await _unitOfWork.ProvinceRepository.ListAsync(countryId);

                // Add to collection
                var list = new List<ProvinceModel>();
                foreach (var item in data)
                {
                    list.Add(_mapper.Map<ProvinceModel>(item));
                }

                // Set response
                response.Data = list;
            }
            catch (Exception ex)
            {
                _logger.LogError("ProvinceRequestService.ProcessListRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
