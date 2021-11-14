using Api.Models.Dto;
using Api.Models.Results;
using AutoMapper;
using Data;
using Data.Enums;
using Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services;

public class CategoryEntityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryEntityService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<CategoryEntityService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ServiceResult<IEnumerable<CategoryModel>>> ListAsync(CategoryType categoryType, int tenantId)
    {
        var result = new ServiceResult<IEnumerable<CategoryModel>>();

        // Fetch data
        var data = await _unitOfWork.CategoryRepository.ListAsync(categoryType, tenantId);

        // Add to collection
        var list = new List<CategoryModel>();
        foreach (var item in data)
        {
            list.Add(_mapper.Map<CategoryModel>(item));
        }

        result.Data = list;

        return result;
    }

    public async Task<ServiceResult<CategoryModel>> GetModelOrDefaultAsync(int id, int tenantId)
    {
        var result = new ServiceResult<CategoryModel>();

        // Fetch object
        var category = await GetEntityOrDefaultAsync(id, tenantId);
        if (category == null)
        {
            result.SetNotFound($"Unable to locate Category object ({id})");
            return result;
        }

        result.Data = _mapper.Map<CategoryModel>(category);

        return result;
    }

    public async Task<ServiceResult<CategoryModel>> CreateAsync(string name, CategoryType categoryType, UserProfile user)
    {
        var result = new ServiceResult<CategoryModel>();

        var now = DateTime.UtcNow;

        // Build and add the new object
        var category = new Category
        {
            Name = name,
            Type = categoryType,
            TenantId = user.TenantId.Value,
            CreatedUserId = user.Id,
            CreatedUtc = now,
            LastModifiedUserId = user.Id,
            LastModifiedUtc = now
        };
        await _unitOfWork.CategoryRepository.AddAsync(category);

        // Set response
        if (await _unitOfWork.CompleteAsync() <= 0)
        {
            result.SetError("Failed to create Category");
            return result;
        }

        result.Data = _mapper.Map<CategoryModel>(category);

        return result;
    }

    public async Task<ServiceResult> UpdateAsync(int id, string name, CategoryType type, UserProfile user)
    {
        var result = new ServiceResult();

        // Fetch the existing object
        var entity = await GetEntityOrDefaultAsync(id, user.TenantId.Value);
        if (entity == null)
        {
            result.SetNotFound($"Unable to locate Category object ({id})");
            return result;
        }

        // Update entity
        entity.Name = name;
        entity.Type = type;
        entity.LastModifiedUserId = user.Id;
        entity.LastModifiedUtc = DateTime.UtcNow;

        if (await _unitOfWork.CompleteAsync() <= 0) result.SetError("Failed to update Category");

        return result;
    }

    public async Task<ServiceResult> DeleteAsync(int id, UserProfile user)
    {
        var result = new ServiceResult();

        var entity = await GetEntityOrDefaultAsync(id, user.TenantId.Value);
        if (entity == null)
        {
            result.SetNotFound();
            return result;
        }

        _unitOfWork.CategoryRepository.Remove(entity);

        if (await _unitOfWork.CompleteAsync() <= 0) result.SetError("Failed to delete Category");

        return result;
    }

    private async Task<Category> GetEntityOrDefaultAsync(int id, int tenantId)
    {
        return await _unitOfWork.CategoryRepository.GetAsync(id, tenantId);
    }
}
