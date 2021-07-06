using Api.Handlers;
using Api.Models.Dto;
using Api.Models.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class TenantRequestService
    {
        private readonly TenantEntityService _tenantEntityService;

        public TenantRequestService(
            TenantEntityService tenantEntityService)
        {
            _tenantEntityService = tenantEntityService;
        }

        public async Task<ResponseHandler<IEnumerable<TenantModel>>> ProcessListRequestAsync()
        {
            var response = new ResponseHandler<IEnumerable<TenantModel>>();

            response.Data = await _tenantEntityService.ListAsync();

            return response;
        }

        public async Task<ResponseHandler<TenantModel>> ProcessGetRequestAsync(int id)
        {
            var response = new ResponseHandler<TenantModel>();

            // Fetch object
            response.Data = await _tenantEntityService.GetModelOrDefaultAsync(id);
            if (response.Data == null)
            {
                response.SetNotFound($"Unable to locate Tenant object ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler<TenantModel>> ProcessCreateRequestAsync(TenantRequest model, int createdByUserId)
        {
            var response = new ResponseHandler<TenantModel>();

            response.Data = await _tenantEntityService.CreateAsync(model, createdByUserId);
            if (response.Data == null)
            {
                response.SetError("An unexpected error occurred while saving the Tenant object");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessUpdateRequestAsync(int id, TenantRequest model, int modifiedByUserId)
        {
            var response = new ResponseHandler();

            // Fetch the existing object
            var tenant = await _tenantEntityService.GetEntityOrDefaultAsync(id);
            if (tenant != null)
            {
                if (!(await _tenantEntityService.UpdateAsync(tenant, model, modifiedByUserId)))
                {
                    response.SetError("An unexpected error occurred while saving the Tenant object");
                }
            }
            else
            {
                response.SetNotFound($"Unable to locate Tenant object ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessDeleteRequestAsync(int id, int deletedByUserId)
        {
            var response = new ResponseHandler();

            // Fetch the existing object
            var tenant = await _tenantEntityService.GetEntityOrDefaultAsync(id);
            if (tenant != null)
            {
                if (!await _tenantEntityService.DeleteAsync(tenant, deletedByUserId))
                {
                    response.SetError("An unexpected error occurred while removing the Tenant object");
                }
            }
            else
            {
                response.SetNotFound($"Unable to locate Tenant object ({id})");
            }

            return response;
        }
    }
}
