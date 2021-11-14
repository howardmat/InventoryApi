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

public class ProductInventoryTransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ResourceAuthorization<ProductAuthorizationProvider> _productAuthorizationProvider;

    public ProductInventoryTransactionService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ResourceAuthorization<ProductAuthorizationProvider> productAuthorizationProvider)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productAuthorizationProvider = productAuthorizationProvider;
    }

    public async Task<ServiceResult<IEnumerable<ProductInventoryTransactionModel>>> ListAsync(int productId, int tenantId)
    {
        var response = new ServiceResult<IEnumerable<ProductInventoryTransactionModel>>();

        if (!await _productAuthorizationProvider.TenantHasResourceAccessAsync(tenantId, productId))
        {
            response.SetNotFound($"ProductId [{productId}] is invalid");
            return response;
        }

        // Fetch data
        var data = await _unitOfWork.ProductInventoryTransactionRepository.ListAsync(productId, tenantId);

        // Add to collection
        var list = new List<ProductInventoryTransactionModel>();
        foreach (var item in data)
        {
            list.Add(_mapper.Map<ProductInventoryTransactionModel>(item));
        }

        response.Data = list;

        return response;
    }

    public async Task<ServiceResult<ProductInventoryTransactionModel>> GetModelOrDefaultAsync(int id, int tenantId)
    {
        var response = new ServiceResult<ProductInventoryTransactionModel>();

        // Fetch object
        var productInventoryTransaction = await GetEntityOrDefaultAsync(id, tenantId);

        // Set response
        if (productInventoryTransaction == null)
        {
            response.SetNotFound($"Unable to locate Product Inventory record ({id})");
            return response;
        }

        response.Data = _mapper.Map<ProductInventoryTransactionModel>(productInventoryTransaction);

        return response;
    }

    public async Task<ServiceResult<ProductInventoryTransactionModel>> CreateAsync(ProductInventoryTransactionRequest model, UserProfile user)
    {
        var response = new ServiceResult<ProductInventoryTransactionModel>();

        if (!await _productAuthorizationProvider.TenantHasResourceAccessAsync(user.TenantId.Value, model.ProductId.Value))
        {
            response.SetNotFound($"ProductId [{model.ProductId}] is invalid");
            return response;
        }

        var now = DateTime.UtcNow;

        // Build and add the new object
        var transaction = new ProductInventoryTransaction
        {
            ProductId = model.ProductId.Value,
            Quantity = model.Quantity,
            OrderDetailId = model.OrderDetailId,
            Description = model.Description,
            CreatedUserId = user.Id,
            LastModifiedUserId = user.Id,
            TenantId = user.TenantId.Value,
            CreatedUtc = now,
            LastModifiedUtc = now
        };
        await _unitOfWork.ProductInventoryTransactionRepository.AddAsync(transaction);

        // Set response
        if (await _unitOfWork.CompleteAsync() <= 0)
        {
            response.SetError("An unexpected error occurred while saving the Product Inventory record");
            return response;
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
            response.SetNotFound($"Unable to locate Product object ({id})");
            return response;
        }

        _unitOfWork.ProductInventoryTransactionRepository.Remove(entity);

        if (await _unitOfWork.CompleteAsync() <= 0)
        {
            response.SetError("An unexpected error occurred while removing the Product Inventory record");
        }

        return response;
    }

    private async Task<ProductInventoryTransaction> GetEntityOrDefaultAsync(int id, int tenantId)
    {
        // Fetch object
        return await _unitOfWork.ProductInventoryTransactionRepository.GetAsync(id, tenantId);
    }
}
