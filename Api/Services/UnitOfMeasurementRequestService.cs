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

        public async Task<ResponseHandler<UnitOfMeasurementModel>> ProcessCreateRequestAsync(UnitOfMeasurementModel model, int createdByUserId)
        {
            var response = new ResponseHandler<UnitOfMeasurementModel>();

            response.Data = await _unitOfMeasurementEntityService.CreateAsync(model.Name, model.Abbreviation, model.System, model.Type, createdByUserId);
            if (response.Data == null)
            {
                response.SetError("An unexpected error occurred while saving the UnitOfMeasurement object");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessUpdateRequestAsync(int id, UnitOfMeasurementModel model, int modifiedByUserId)
        {
            var response = new ResponseHandler();

            // Fetch the existing object
            var unitOfMeasurement = await _unitOfMeasurementEntityService.GetEntityOrDefaultAsync(id);
            if (unitOfMeasurement != null)
            {
                if (!await _unitOfMeasurementEntityService.UpdateAsync(unitOfMeasurement, model.Name, model.Abbreviation, modifiedByUserId))
                {
                    response.SetError("An unexpected error occurred while saving the UnitOfMeasurement object");
                }
            }
            else
            {
                response.SetNotFound($"Unable to locate UnitOfMeasurement object ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessDeleteRequestAsync(int id, int deletedByUserId)
        {
            var response = new ResponseHandler();

            // Fetch the existing object
            var unitOfMeasurement = await _unitOfMeasurementEntityService.GetEntityOrDefaultAsync(id);
            if (unitOfMeasurement != null)
            {
                if (!await _unitOfMeasurementEntityService.DeleteAsync(unitOfMeasurement, deletedByUserId))
                {
                    response.SetError("An unexpected error occurred while removing the UnitOfMeasurement object");
                }
            }
            else
            {
                response.SetNotFound($"Unable to locate UnitOfMeasurement object ({id})");
            }

            return response;
        }
    }
}
