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

public class ProductEntityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ResourceAuthorization<CategoryAuthorizationProvider> _categoryAuthorizationProvider;
    private readonly ResourceAuthorization<FormulaAuthorizationProvider> _formulaAuthorizationProvider;

    public ProductEntityService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ResourceAuthorization<CategoryAuthorizationProvider> categoryAuthorizationProvider,
        ResourceAuthorization<FormulaAuthorizationProvider> formulaAuthorizationProvider)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _categoryAuthorizationProvider = categoryAuthorizationProvider;
        _formulaAuthorizationProvider = formulaAuthorizationProvider;
    }

    public async Task<ServiceResult<IEnumerable<ProductModel>>> ListAsync(int tenantId)
    {
        var response = new ServiceResult<IEnumerable<ProductModel>>();

        // Fetch data
        var data = await _unitOfWork.ProductRepository.ListAsync(tenantId);

        // Add to collection
        var list = new List<ProductModel>();
        foreach (var item in data)
        {
            list.Add(_mapper.Map<ProductModel>(item));
        }

        response.Data = list;

        return response;
    }

    public async Task<ServiceResult<ProductModel>> GetModelOrDefaultAsync(int id, int tenantId)
    {
        var response = new ServiceResult<ProductModel>();

        // Fetch object
        var product = await _unitOfWork.ProductRepository.GetAsync(id, tenantId);

        // Set response
        if (product == null)
        {
            response.SetNotFound($"Unable to locate Product object ({id})");
            return response;
        }
        
        response.Data = _mapper.Map<ProductModel>(product);

        return response;
    }

    public async Task<ServiceResult<ProductModel>> CreateAsync(ProductRequest model, UserProfile user)
    {
        var response = new ServiceResult<ProductModel>();

        if (!await _categoryAuthorizationProvider.TenantHasResourceAccessAsync(user.TenantId.Value, model.CategoryId.Value))
        {
            response.SetNotFound($"CategoryId [{model.CategoryId}] is invalid");
            return response;
        }

        if (model.FormulaId.HasValue
            && !await _formulaAuthorizationProvider.TenantHasResourceAccessAsync(user.TenantId.Value, model.FormulaId.Value))
        {
            response.SetNotFound($"FormulaId [{model.FormulaId}] is invalid");
            return response;
        }

        var now = DateTime.UtcNow;

        // Build and add the new object
        var product = new Product
        {
            Name = model.Name,
            CategoryId = model.CategoryId.Value,
            Description = model.Description,
            UnitOfMeasurementId = model.UnitOfMeasurementId.Value,
            CreatedUserId = user.Id,
            LastModifiedUserId = user.Id,
            TenantId = user.TenantId.Value,
            CreatedUtc = now,
            LastModifiedUtc = now
        };
        await _unitOfWork.ProductRepository.AddAsync(product);

        // Set response
        if (await _unitOfWork.CompleteAsync() <= 0)
        {
            response.SetError("An unexpected error occurred while saving the Product object");
            return response;
        }

        var modelResult = await GetModelOrDefaultAsync(product.Id, user.TenantId.Value);
        response.Data = modelResult.Data;

        return response;
    }

    public async Task<ServiceResult> UpdateAsync(int id, ProductRequest model, UserProfile user)
    {
        var response = new ServiceResult();

        if (!await _categoryAuthorizationProvider.TenantHasResourceAccessAsync(user.TenantId.Value, model.CategoryId.Value))
        {
            response.SetNotFound($"CategoryId [{model.CategoryId}] is invalid");
            return response;
        }

        if (model.FormulaId.HasValue
            && !await _formulaAuthorizationProvider.TenantHasResourceAccessAsync(user.TenantId.Value, model.FormulaId.Value))
        {
            response.SetNotFound($"FormulaId [{model.FormulaId}] is invalid");
            return response;
        }

        // Fetch the existing object
        var product = await GetEntityOrDefaultAsync(id, user.TenantId.Value);
        if (product == null)
        {
            response.SetNotFound($"Unable to locate Product object ({id})");
            return response;
        }
        
        // Update properties
        product.Name = model.Name;
        product.CategoryId = model.CategoryId.Value;
        product.Description = model.Description;
        product.UnitOfMeasurementId = model.UnitOfMeasurementId.Value;
        product.LastModifiedUserId = user.Id;
        product.LastModifiedUtc = DateTime.UtcNow;

        // Set response
        if (await _unitOfWork.CompleteAsync() <= 0)
        {
            response.SetError("An unexpected error occurred while saving the Product object");
        }

        return response;
    }

    public async Task<ServiceResult> DeleteAsync(int id, UserProfile user)
    {
        var response = new ServiceResult();

        // Fetch the existing object
        var product = await GetEntityOrDefaultAsync(id, user.TenantId.Value);
        if (product == null)
        {
            response.SetNotFound($"Unable to locate Product object ({id})");
            return response;
        }
        
        _unitOfWork.ProductRepository.Remove(product);

        if (await _unitOfWork.CompleteAsync() <= 0)
        {
            response.SetError("An unexpected error occurred while removing the Product object");
        }

        return response;
    }

    private async Task<Product> GetEntityOrDefaultAsync(int id, int tenantId)
    {
        // Fetch object
        return await _unitOfWork.ProductRepository.GetAsync(id, tenantId);
    }
}
