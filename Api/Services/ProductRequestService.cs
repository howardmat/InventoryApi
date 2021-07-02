using Api.Handlers;
using Api.Models.Dto;
using Api.Models.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ProductRequestService
    {
        private readonly ProductEntityService _productEntityService;
        public ProductRequestService(
            ProductEntityService productEntityService)
        {
            _productEntityService = productEntityService;
        }

        public async Task<ResponseHandler<IEnumerable<ProductModel>>> ProcessListRequestAsync(int tenantId)
        {
            var response = new ResponseHandler<IEnumerable<ProductModel>>();

            response.Data = await _productEntityService.ListAsync(tenantId);

            return response;
        }

        public async Task<ResponseHandler<ProductModel>> ProcessGetRequestAsync(int id, int tenantId)
        {
            var response = new ResponseHandler<ProductModel>();

            response.Data = await _productEntityService.GetModelOrDefaultAsync(id, tenantId);
            if (response.Data == null)
            {
                response.SetNotFound($"Unable to locate Product object ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler<ProductModel>> ProcessCreateRequestAsync(ProductRequest model, int createdByUserId, int tenantId)
        {
            var response = new ResponseHandler<ProductModel>();

            response.Data = await _productEntityService.CreateAsync(model, createdByUserId, tenantId);
            if (response.Data == null)
            {
                response.SetError("An unexpected error occurred while saving the Product object");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessUpdateRequestAsync(int id, ProductRequest model, int modifiedByUserId, int tenantId)
        {
            var response = new ResponseHandler();

            // Fetch the existing object
            var product = await _productEntityService.GetEntityOrDefaultAsync(id, tenantId);
            if (product != null)
            {
                if (!await _productEntityService.UpdateAsync(product, model, modifiedByUserId))
                {
                    response.SetError("An unexpected error occurred while saving the Product object");
                }
            }
            else
            {
                response.SetNotFound($"Unable to locate Product object ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessDeleteRequestAsync(int id, int deletedByUserId, int tenantId)
        {
            var response = new ResponseHandler();

            // Fetch the existing object
            var product = await _productEntityService.GetEntityOrDefaultAsync(id, tenantId);
            if (product != null)
            {
                if (!await _productEntityService.DeleteAsync(product, deletedByUserId))
                {
                    response.SetError("An unexpected error occurred while removing the Product object");
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
