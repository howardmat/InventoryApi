using Api.Models.Dto;
using Api.Models.Results;
using AutoMapper;
using Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ProvinceEntityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProvinceEntityService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<ProvinceModel>>> ListAsync(string countryCode)
        {
            var response = new ServiceResult<IEnumerable<ProvinceModel>>();

            // Fetch data
            var data = await _unitOfWork.ProvinceRepository.ListAsync(countryCode);

            // Add to collection
            var list = new List<ProvinceModel>();
            foreach (var item in data)
            {
                list.Add(_mapper.Map<ProvinceModel>(item));
            }

            response.Data = list;

            return response;
        }

        public async Task<ServiceResult<ProvinceModel>> GetAsync(string countryIsoCode, string provinceCode)
        {
            var response = new ServiceResult<ProvinceModel>();

            // Fetch data
            var item = await _unitOfWork.ProvinceRepository.GetAsync(countryIsoCode, provinceCode);
            response.Data = _mapper.Map<ProvinceModel>(item);

            return response;
        }
    }
}
