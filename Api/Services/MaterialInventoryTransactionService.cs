using Api.Models.Dto;
using Api.Models.RequestModels;
using AutoMapper;
using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class MaterialInventoryTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaterialInventoryTransactionService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialInventoryTransactionModel>> ListAsync(int materialId, int tenantId)
        {
            // Fetch data
            var data = await _unitOfWork.MaterialInventoryTransactionRepository.ListAsync(materialId, tenantId);

            // Add to collection
            var list = new List<MaterialInventoryTransactionModel>();
            foreach (var item in data)
            {
                list.Add(_mapper.Map<MaterialInventoryTransactionModel>(item));
            }

            return list;
        }

        public async Task<MaterialInventoryTransaction> GetEntityOrDefaultAsync(int id, int tenantId)
        {
            // Fetch object
            var entity = await _unitOfWork.MaterialInventoryTransactionRepository.GetAsync(id, tenantId);

            return entity;
        }

        public async Task<MaterialInventoryTransactionModel> GetModelOrDefaultAsync(int id, int tenantId)
        {
            MaterialInventoryTransactionModel model = null;

            // Fetch object
            var materialInventoryTransaction = await GetEntityOrDefaultAsync(id, tenantId);

            // Set response
            if (materialInventoryTransaction != null)
            {
                model = _mapper.Map<MaterialInventoryTransactionModel>(materialInventoryTransaction);
            }

            return model;
        }

        public async Task<MaterialInventoryTransactionModel> CreateAsync(MaterialInventoryTransactionRequest model, int modifyingUserId, int tenantId)
        {
            MaterialInventoryTransactionModel newModel = null;

            var now = DateTime.UtcNow;

            // Build and add the new object
            var transaction = new MaterialInventoryTransaction
            {
                MaterialId = model.MaterialId.Value,
                Quantity = model.Quantity,
                AmountPaid = model.AmountPaid,
                Description = model.Description,
                CreatedUserId = modifyingUserId,
                LastModifiedUserId = modifyingUserId,
                TenantId = tenantId,
                CreatedUtc = now,
                LastModifiedUtc = now
            };
            await _unitOfWork.MaterialInventoryTransactionRepository.AddAsync(transaction);

            // Set response
            if (await _unitOfWork.CompleteAsync() > 0)
            {
                newModel = await GetModelOrDefaultAsync(transaction.Id, tenantId);
            }

            return newModel;
        }

        public async Task<bool> DeleteAsync(MaterialInventoryTransaction materialTransaction, int modifyingUserId)
        {
            var now = DateTime.UtcNow;

            // Update entity
            materialTransaction.DeletedUserId = modifyingUserId;
            materialTransaction.DeletedUtc = now;

            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }
    }
}
