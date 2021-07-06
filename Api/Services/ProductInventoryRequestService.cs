using Api.Authorization;
using Api.Handlers;
using Api.Models.Dto;
using Api.Models.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ProductInventoryRequestService
    {
        private readonly ProductInventoryTransactionService _productInventoryTransactionService;
        private readonly ResourceAuthorization<ProductAuthorizationProvider> _productAuthorizationProvider;

        public ProductInventoryRequestService(
            ProductInventoryTransactionService productInventoryTransactionService,
            ResourceAuthorization<ProductAuthorizationProvider> productAuthorizationProvider)
        {
            _productInventoryTransactionService = productInventoryTransactionService;
            _productAuthorizationProvider = productAuthorizationProvider;
        }

        public async Task<ResponseHandler<IEnumerable<ProductInventoryTransactionModel>>> ProcessListRequestAsync(int productId, int tenantId)
        {
            var response = new ResponseHandler<IEnumerable<ProductInventoryTransactionModel>>();

            response.Data = await _productInventoryTransactionService.ListAsync(productId, tenantId);

            return response;
        }

        public async Task<ResponseHandler<ProductInventoryTransactionModel>> ProcessGetRequestAsync(int id, int tenantId)
        {
            var response = new ResponseHandler<ProductInventoryTransactionModel>();

            response.Data = await _productInventoryTransactionService.GetModelOrDefaultAsync(id, tenantId);
            if (response.Data == null)
            {
                response.SetNotFound($"Unable to locate Product Inventory record ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler<ProductInventoryTransactionModel>> ProcessCreateRequestAsync(ProductInventoryTransactionRequest model, int createdByUserId, int tenantId)
        {
            var response = new ResponseHandler<ProductInventoryTransactionModel>();

            if (!await _productAuthorizationProvider.TenantHasResourceAccessAsync(tenantId, model.ProductId.Value))
            {
                response.SetNotFound($"ProductId [{model.ProductId}] is invalid");
                return response;
            }

            response.Data = await _productInventoryTransactionService.CreateAsync(model, createdByUserId, tenantId);
            if (response.Data == null)
            {
                response.SetError("An unexpected error occurred while saving the Product Inventory record");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessDeleteRequestAsync(int id, int deletedByUserId, int tenantId)
        {
            var response = new ResponseHandler();

            var entity = await _productInventoryTransactionService.GetEntityOrDefaultAsync(id, tenantId);
            if (entity != null)
            {
                if (!await _productInventoryTransactionService.DeleteAsync(entity, deletedByUserId))
                {
                    response.SetError("An unexpected error occurred while removing the Product Inventory record");
                }
            }
            else
            {
                response.SetNotFound($"Unable to locate Product object ({id})");
            }

            return response;
        }
    }
}
