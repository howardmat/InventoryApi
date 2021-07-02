using Api.Handlers;
using Api.Models.Dto;
using Api.Models.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class MaterialRequestService
    {
        private readonly MaterialEntityService _materialEntityService;
        public MaterialRequestService(
            MaterialEntityService materialEntityService)
        {
            _materialEntityService = materialEntityService;
        }

        public async Task<ResponseHandler<IEnumerable<MaterialModel>>> ProcessListRequestAsync(int tenantId)
        {
            var response = new ResponseHandler<IEnumerable<MaterialModel>>();

            response.Data = await _materialEntityService.ListAsync(tenantId);

            return response;
        }

        public async Task<ResponseHandler<MaterialModel>> ProcessGetRequestAsync(int id, int tenantId)
        {
            var response = new ResponseHandler<MaterialModel>();

            response.Data = await _materialEntityService.GetModelOrDefaultAsync(id, tenantId);
            if (response.Data == null)
            {
                response.SetNotFound($"Unable to locate Material object ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler<MaterialModel>> ProcessCreateRequestAsync(MaterialRequest model, int createdByUserId, int tenantId)
        {
            var response = new ResponseHandler<MaterialModel>();

            response.Data = await _materialEntityService.CreateAsync(model, createdByUserId, tenantId);
            if (response.Data == null)
            {
                response.SetError("An unexpected error occurred while saving the Material object");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessUpdateRequestAsync(int id, MaterialRequest model, int modifiedByUserId, int tenantId)
        {
            var response = new ResponseHandler();

            // Fetch the existing object
            var material = await _materialEntityService.GetEntityOrDefaultAsync(id, tenantId);
            if (material != null)
            {
                if (!await _materialEntityService.UpdateAsync(material, model, modifiedByUserId))
                {
                    response.SetError("An unexpected error occurred while saving the Material object");
                }
            }
            else
            {
                response.SetNotFound($"Unable to locate Material object ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessDeleteRequestAsync(int id, int deletedByUserId, int tenantId)
        {
            var response = new ResponseHandler();

            // Fetch the existing object
            var material = await _materialEntityService.GetEntityOrDefaultAsync(id, tenantId);
            if (material != null)
            {
                if (!await _materialEntityService.DeleteAsync(material, deletedByUserId))
                {
                    response.SetError("An unexpected error occurred while removing the Material object");
                }
            }
            else
            {
                response.SetNotFound($"Unable to locate Material object ({id})");
            }

            return response;
        }
    }
}
