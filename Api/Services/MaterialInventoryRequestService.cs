using Api.Authorization;
using Api.Handlers;
using Api.Models.Dto;
using Api.Models.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class MaterialInventoryRequestService
    {
        private readonly MaterialInventoryTransactionService _materialInventoryTransactionService;
        private readonly ResourceAuthorization<MaterialAuthorizationProvider> _materialAuthorizationProvider;

        public MaterialInventoryRequestService(
            MaterialInventoryTransactionService materialInventoryTransactionService,
            ResourceAuthorization<MaterialAuthorizationProvider> materialAuthorizationProvider)
        {
            _materialInventoryTransactionService = materialInventoryTransactionService;
            _materialAuthorizationProvider = materialAuthorizationProvider;
        }

        public async Task<ResponseHandler<IEnumerable<MaterialInventoryTransactionModel>>> ProcessListRequestAsync(int materialId, int tenantId)
        {
            var response = new ResponseHandler<IEnumerable<MaterialInventoryTransactionModel>>();

            if (!await _materialAuthorizationProvider.TenantHasResourceAccessAsync(tenantId, materialId))
            {
                response.SetNotFound($"MaterialId [{materialId}] is invalid");
                return response;
            }

            response.Data = await _materialInventoryTransactionService.ListAsync(materialId, tenantId);

            return response;
        }

        public async Task<ResponseHandler<MaterialInventoryTransactionModel>> ProcessGetRequestAsync(int id, int tenantId)
        {
            var response = new ResponseHandler<MaterialInventoryTransactionModel>();

            response.Data = await _materialInventoryTransactionService.GetModelOrDefaultAsync(id, tenantId);
            if (response.Data == null)
            {
                response.SetNotFound($"Unable to locate Material Inventory record ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler<MaterialInventoryTransactionModel>> ProcessCreateRequestAsync(MaterialInventoryTransactionRequest model, int createdByUserId, int tenantId)
        {
            var response = new ResponseHandler<MaterialInventoryTransactionModel>();

            if (!await _materialAuthorizationProvider.TenantHasResourceAccessAsync(tenantId, model.MaterialId.Value))
            {
                response.SetNotFound($"MaterialId [{model.MaterialId}] is invalid");
                return response;
            }

            response.Data = await _materialInventoryTransactionService.CreateAsync(model, createdByUserId, tenantId);
            if (response.Data == null)
            {
                response.SetError("An unexpected error occurred while saving the Material Inventory record");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessDeleteRequestAsync(int id, int deletedByUserId, int tenantId)
        {
            var response = new ResponseHandler();

            var entity = await _materialInventoryTransactionService.GetEntityOrDefaultAsync(id, tenantId);
            if (entity != null)
            {
                if (!await _materialInventoryTransactionService.DeleteAsync(entity, deletedByUserId))
                {
                    response.SetError("An unexpected error occurred while removing the Material Inventory record");
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
