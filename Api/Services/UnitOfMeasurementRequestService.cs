using Api.Handlers;
using Api.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class UnitOfMeasurementRequestService
    {
        private readonly UnitOfMeasurementEntityService _unitOfMeasurementEntityService;

        public UnitOfMeasurementRequestService(
            UnitOfMeasurementEntityService unitOfMeasurementEntityService)
        {
            _unitOfMeasurementEntityService = unitOfMeasurementEntityService;
        }

        public async Task<ResponseHandler<IEnumerable<UnitOfMeasurementModel>>> ProcessListRequestAsync()
        {
            var response = new ResponseHandler<IEnumerable<UnitOfMeasurementModel>>();

            response.Data = await _unitOfMeasurementEntityService.ListAsync();

            return response;
        }

        public async Task<ResponseHandler<UnitOfMeasurementModel>> ProcessGetRequestAsync(int id)
        {
            var response = new ResponseHandler<UnitOfMeasurementModel>();

            // Fetch object
            response.Data = await _unitOfMeasurementEntityService.GetModelOrDefaultAsync(id);
            if (response.Data == null)
            {
                response.SetNotFound($"Unable to locate UnitOfMeasurement object ({id})");
            }

            return response;
        }
    }
}
