using Api.Authorization;
using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Models.Results;
using AutoMapper;
using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services;

public class MaterialInventoryTransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ResourceAuthorization<MaterialAuthorizationProvider> _materialAuthorizationProvider;

    public MaterialInventoryTransactionService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ResourceAuthorization<MaterialAuthorizationProvider> materialAuthorizationProvider)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _materialAuthorizationProvider = materialAuthorizationProvider;
    }

    public async Task<ServiceResult<IEnumerable<MaterialInventoryTransactionModel>>> ListAsync(int materialId, int tenantId)
    {
        var response = new ServiceResult<IEnumerable<MaterialInventoryTransactionModel>>();

        if (!await _materialAuthorizationProvider.TenantHasResourceAccessAsync(tenantId, materialId))
        {
            response.SetNotFound($"MaterialId [{materialId}] is invalid");
            return response;
        }

        // Fetch data
        var data = await _unitOfWork.MaterialInventoryTransactionRepository.ListAsync(materialId, tenantId);

        // Add to collection
        var list = new List<MaterialInventoryTransactionModel>();
        foreach (var item in data)
        {
            list.Add(_mapper.Map<MaterialInventoryTransactionModel>(item));
        }

        response.Data = list;

        return response;
    }

    public async Task<ServiceResult<MaterialInventoryTransactionModel>> GetModelOrDefaultAsync(int id, int tenantId)
    {
        var response = new ServiceResult<MaterialInventoryTransactionModel>();

        // Fetch object
        var materialInventoryTransaction = await GetEntityOrDefaultAsync(id, tenantId);

        // Set response
        if (materialInventoryTransaction == null)
        {
            response.SetNotFound($"Unable to locate Material Inventory record ({id})");
            return response;
        }

        response.Data = _mapper.Map<MaterialInventoryTransactionModel>(materialInventoryTransaction);

        return response;
    }

    public async Task<ServiceResult<MaterialInventoryTransactionModel>> CreateAsync(MaterialInventoryTransactionRequest model, UserProfile user)
    {
        var response = new ServiceResult<MaterialInventoryTransactionModel>();

        if (!await _materialAuthorizationProvider.TenantHasResourceAccessAsync(user.TenantId.Value, model.MaterialId.Value))
        {
            response.SetNotFound($"MaterialId [{model.MaterialId}] is invalid");
            return response;
        }

        var now = DateTime.UtcNow;

        // Build and add the new object
        var transaction = new MaterialInventoryTransaction
        {
            MaterialId = model.MaterialId.Value,
            Quantity = model.Quantity,
            AmountPaid = model.AmountPaid,
            Description = model.Description,
            CreatedUserId = user.Id,
            LastModifiedUserId = user.Id,
            TenantId = user.TenantId.Value,
            CreatedUtc = now,
            LastModifiedUtc = now
        };
        await _unitOfWork.MaterialInventoryTransactionRepository.AddAsync(transaction);

        // Set response
        if (await _unitOfWork.CompleteAsync() <= 0)
        {
            response.SetError("An unexpected error occurred while saving the Material Inventory record");
        }

        var modelResult = await GetModelOrDefaultAsync(transaction.Id, user.TenantId.Value);
        response.Data = modelResult.Data;

        return response;
    }

    public async Task<ServiceResult> DeleteAsync(int id, UserProfile user)
    {
        var response = new ServiceResult();

        var entity = await GetEntityOrDefaultAsync(id, user.TenantId.Value);
        if (entity == null)
        {
            response.SetNotFound($"Unable to locate Material object ({id})");
            return response;
        }
        
        _unitOfWork.MaterialInventoryTransactionRepository.Remove(entity);

        if (await _unitOfWork.CompleteAsync() <= 0)
        {
            response.SetError("An unexpected error occurred while removing the Material Inventory record");
        }

        return response;
    }

    private async Task<MaterialInventoryTransaction> GetEntityOrDefaultAsync(int id, int tenantId)
    {
        // Fetch object
        return await _unitOfWork.MaterialInventoryTransactionRepository.GetAsync(id, tenantId);
    }
}
