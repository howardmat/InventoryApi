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

public class MaterialEntityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ResourceAuthorization<CategoryAuthorizationProvider> _categoryAuthorizationProvider;

    public MaterialEntityService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ResourceAuthorization<CategoryAuthorizationProvider> categoryAuthorizationProvider)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _categoryAuthorizationProvider = categoryAuthorizationProvider;
    }

    public async Task<ServiceResult<IEnumerable<MaterialModel>>> ListAsync(int tenantId)
    {
        var response = new ServiceResult<IEnumerable<MaterialModel>>();

        // Fetch data
        var data = await _unitOfWork.MaterialRepository.ListAsync(tenantId);

        // Add to collection
        var list = new List<MaterialModel>();
        foreach (var item in data)
        {
            list.Add(_mapper.Map<MaterialModel>(item));
        }

        response.Data = list;

        return response;
    }

    public async Task<ServiceResult<MaterialModel>> GetModelOrDefaultAsync(int id, int tenantId)
    {
        var response = new ServiceResult<MaterialModel>();

        // Fetch object
        var material = await _unitOfWork.MaterialRepository.GetAsync(id, tenantId);

        // Set response
        if (material == null)
        {
            response.SetNotFound($"Unable to locate Material object ({id})");
        }

        response.Data = _mapper.Map<MaterialModel>(material);

        return response;
    }

    public async Task<ServiceResult<MaterialModel>> CreateAsync(MaterialRequest model, UserProfile user)
    {
        var response = new ServiceResult<MaterialModel>();

        if (!await _categoryAuthorizationProvider.TenantHasResourceAccessAsync(user.TenantId.Value, model.CategoryId.Value))
        {
            response.SetNotFound($"CategoryId [{model.CategoryId}] is invalid");
            return response;
        }

        var now = DateTime.UtcNow;

        // Build and add the new object
        var material = new Material
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
        await _unitOfWork.MaterialRepository.AddAsync(material);

        // Set response
        if (await _unitOfWork.CompleteAsync() <= 0)
        {
            response.SetError("An unexpected error occurred while saving the Material object");
        }

        var modelResult = await GetModelOrDefaultAsync(material.Id, user.TenantId.Value);
        response.Data = modelResult.Data;

        return response;
    }

    public async Task<ServiceResult> UpdateAsync(int id, MaterialRequest model, UserProfile user)
    {
        var response = new ServiceResult();

        if (!await _categoryAuthorizationProvider.TenantHasResourceAccessAsync(user.TenantId.Value, model.CategoryId.Value))
        {
            response.SetNotFound($"CategoryId [{model.CategoryId}] is invalid");
            return response;
        }

        // Fetch the existing object
        var material = await GetEntityOrDefaultAsync(id, user.TenantId.Value);
        if (material == null)
        {
            response.SetNotFound($"Unable to locate Material object ({id})");
            return response;
        }

        // Update properties
        material.Name = model.Name;
        material.CategoryId = model.CategoryId.Value;
        material.Description = model.Description;
        material.UnitOfMeasurementId = model.UnitOfMeasurementId.Value;
        material.LastModifiedUserId = user.Id;
        material.LastModifiedUtc = DateTime.UtcNow;

        // Set response
        if (await _unitOfWork.CompleteAsync() <= 0)
        {
            response.SetError("An unexpected error occurred while saving the Material object");
        }

        return response;
    }

    public async Task<ServiceResult> DeleteAsync(int id, UserProfile user)
    {
        var response = new ServiceResult();

        // Fetch the existing object
        var material = await GetEntityOrDefaultAsync(id, user.TenantId.Value);
        if (material == null)
        {
            response.SetNotFound($"Unable to locate Material object ({id})");
            return response;
        }

        _unitOfWork.MaterialRepository.Remove(material);

        if (await _unitOfWork.CompleteAsync() <= 0)
        {
            response.SetError("An unexpected error occurred while removing the Material object");
        }

        return response;
    }

    private async Task<Material> GetEntityOrDefaultAsync(int id, int tenantId)
    {
        // Fetch object
        return await _unitOfWork.MaterialRepository.GetAsync(id, tenantId);
    }
}
