using Api.Models.Dto;
using AutoMapper;
using Data;
using Data.Models;
using System;
using System.Threading.Tasks;

namespace Api.Services
{
    public class MaterialInventoryTransactionEntityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaterialInventoryTransactionEntityService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MaterialInventoryTransactionModel> CreateAsync(MaterialInventoryTransactionModel model, int modifyingUserId, int tenantId)
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
                newModel = _mapper.Map<MaterialInventoryTransactionModel>(transaction);
            }

            return newModel;
        }
    }
}
