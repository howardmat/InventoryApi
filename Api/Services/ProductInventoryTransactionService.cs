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
    public class ProductInventoryTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductInventoryTransactionService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductInventoryTransactionModel>> ListAsync(int productId, int tenantId)
        {
            // Fetch data
            var data = await _unitOfWork.ProductInventoryTransactionRepository.ListAsync(productId, tenantId);

            // Add to collection
            var list = new List<ProductInventoryTransactionModel>();
            foreach (var item in data)
            {
                list.Add(_mapper.Map<ProductInventoryTransactionModel>(item));
            }

            return list;
        }

        public async Task<ProductInventoryTransaction> GetEntityOrDefaultAsync(int id, int tenantId)
        {
            // Fetch object
            var entity = await _unitOfWork.ProductInventoryTransactionRepository.GetAsync(id, tenantId);

            return entity;
        }

        public async Task<ProductInventoryTransactionModel> GetModelOrDefaultAsync(int id, int tenantId)
        {
            ProductInventoryTransactionModel model = null;

            // Fetch object
            var productInventoryTransaction = await GetEntityOrDefaultAsync(id, tenantId);

            // Set response
            if (productInventoryTransaction != null)
            {
                model = _mapper.Map<ProductInventoryTransactionModel>(productInventoryTransaction);
            }

            return model;
        }

        public async Task<ProductInventoryTransactionModel> CreateAsync(ProductInventoryTransactionRequest model, int modifyingUserId, int tenantId)
        {
            ProductInventoryTransactionModel newModel = null;

            var now = DateTime.UtcNow;

            // Build and add the new object
            var transaction = new ProductInventoryTransaction
            {
                ProductId = model.ProductId.Value,
                Quantity = model.Quantity,
                OrderDetailId = model.OrderDetailId,
                Description = model.Description,
                CreatedUserId = modifyingUserId,
                LastModifiedUserId = modifyingUserId,
                TenantId = tenantId,
                CreatedUtc = now,
                LastModifiedUtc = now
            };
            await _unitOfWork.ProductInventoryTransactionRepository.AddAsync(transaction);

            // Set response
            if (await _unitOfWork.CompleteAsync() > 0)
            {
                newModel = _mapper.Map<ProductInventoryTransactionModel>(transaction);
            }

            return newModel;
        }

        public async Task<bool> DeleteAsync(ProductInventoryTransaction productTransaction, int modifyingUserId)
        {
            var now = DateTime.UtcNow;

            // Update entity
            productTransaction.DeletedUserId = modifyingUserId;
            productTransaction.DeletedUtc = now;

            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }
    }
}
