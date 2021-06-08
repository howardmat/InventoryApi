using Api.Models;
using Api.Models.Dto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class MaterialTransactionRequestService
    {
        private readonly ILogger<MaterialTransactionRequestService> _logger;
        private readonly MaterialInventoryTransactionService _materialInventoryTransactionService;
        public MaterialTransactionRequestService(
            ILogger<MaterialTransactionRequestService> logger,
            MaterialInventoryTransactionService materialInventoryTransactionService)
        {
            _logger = logger;
            _materialInventoryTransactionService = materialInventoryTransactionService;
        }

        public async Task<ServiceResponse<IEnumerable<MaterialInventoryTransactionModel>>> ProcessListRequestAsync(int materialId)
        {
            var response = new ServiceResponse<IEnumerable<MaterialInventoryTransactionModel>>();

            try
            {
                response.Data = await _materialInventoryTransactionService.ListAsync(materialId);
            }
            catch (Exception ex)
            {
                _logger.LogError("MaterialTransactionRequestService.ProcessListRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<MaterialInventoryTransactionModel>> ProcessGetRequestAsync(int id)
        {
            var response = new ServiceResponse<MaterialInventoryTransactionModel>();

            try
            {
                response.Data = await _materialInventoryTransactionService.GetModelOrDefaultAsync(id);
                if (response.Data == null)
                {
                    response.SetNotFound($"Unable to locate Material Inventory record ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("MaterialTransactionRequestService.ProcessGetRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<MaterialInventoryTransactionModel>> ProcessCreateRequestAsync(MaterialInventoryTransactionModel model, int createdByUserId, int tenantId)
        {
            var response = new ServiceResponse<MaterialInventoryTransactionModel>();

            try
            {
                response.Data = await _materialInventoryTransactionService.CreateAsync(model, createdByUserId, tenantId);
                if (response.Data == null)
                {
                    response.SetError("An unexpected error occurred while saving the Material Inventory record");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("MaterialTransactionRequestService.ProcessCreateRequestAsync - exception:{@Exception}", ex);

                response.SetException(ex.Message);
            }

            return response;
        }

        public async Task<ServiceResponse> ProcessDeleteRequestAsync(int id, int deletedByUserId)
        {
            var response = new ServiceResponse();

            try
            {
                var entity = await _materialInventoryTransactionService.GetEntityOrDefaultAsync(id);
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
            }
            catch (Exception ex)
            {
                _logger.LogError("MaterialTransactionRequestService.ProcessDeleteRequestAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
