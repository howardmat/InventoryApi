using Api.Models;
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

        public async Task<IEnumerable<ProvinceModel>> ListAsync(string countryCode)
        {
            // Fetch data
            var data = await _unitOfWork.ProvinceRepository.ListAsync(countryCode);

            // Add to collection
            var list = new List<ProvinceModel>();
            foreach (var item in data)
            {
                list.Add(_mapper.Map<ProvinceModel>(item));
            }

            return list;
        }

        public async Task<ProvinceModel> GetAsync(string provinceCode)
        {
            // Fetch data
            var item = await _unitOfWork.ProvinceRepository.GetAsync(provinceCode);
            var model = _mapper.Map<ProvinceModel>(item);

            return model;
        }
    }
}
