﻿using Api.Handlers;
using Api.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class MaterialTransactionRequestService
    {
        private readonly MaterialInventoryTransactionService _materialInventoryTransactionService;
        public MaterialTransactionRequestService(
            MaterialInventoryTransactionService materialInventoryTransactionService)
        {
            _materialInventoryTransactionService = materialInventoryTransactionService;
        }

        public async Task<ResponseHandler<IEnumerable<MaterialInventoryTransactionModel>>> ProcessListRequestAsync(int materialId)
        {
            var response = new ResponseHandler<IEnumerable<MaterialInventoryTransactionModel>>();

            response.Data = await _materialInventoryTransactionService.ListAsync(materialId);

            return response;
        }

        public async Task<ResponseHandler<MaterialInventoryTransactionModel>> ProcessGetRequestAsync(int id)
        {
            var response = new ResponseHandler<MaterialInventoryTransactionModel>();

            response.Data = await _materialInventoryTransactionService.GetModelOrDefaultAsync(id);
            if (response.Data == null)
            {
                response.SetNotFound($"Unable to locate Material Inventory record ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler<MaterialInventoryTransactionModel>> ProcessCreateRequestAsync(MaterialInventoryTransactionModel model, int createdByUserId, int tenantId)
        {
            var response = new ResponseHandler<MaterialInventoryTransactionModel>();

            response.Data = await _materialInventoryTransactionService.CreateAsync(model, createdByUserId, tenantId);
            if (response.Data == null)
            {
                response.SetError("An unexpected error occurred while saving the Material Inventory record");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessDeleteRequestAsync(int id, int deletedByUserId)
        {
            var response = new ResponseHandler();

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

            return response;
        }
    }
}