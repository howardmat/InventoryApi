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

public class FormulaEntityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ResourceAuthorization<MaterialAuthorizationProvider> _materialAuthorizationProvider;
    private readonly ResourceAuthorization<CategoryAuthorizationProvider> _categoryAuthorizationProvider;

    public FormulaEntityService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ResourceAuthorization<MaterialAuthorizationProvider> materialAuthorizationProvider,
        ResourceAuthorization<CategoryAuthorizationProvider> categoryAuthorizationProvider)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _materialAuthorizationProvider = materialAuthorizationProvider;
        _categoryAuthorizationProvider = categoryAuthorizationProvider;
    }

    public async Task<ServiceResult<IEnumerable<FormulaModel>>> ListAsync(int tenantId)
    {
        // Fetch data
        var data = await _unitOfWork.FormulaRepository.ListAsync(tenantId);

        // Add to collection
        var list = new List<FormulaModel>();
        foreach (var item in data)
        {
            list.Add(_mapper.Map<FormulaModel>(item));
        }

        var result = new ServiceResult<IEnumerable<FormulaModel>>()
        {
            Data = list
        };

        return result;
    }

    public async Task<ServiceResult<FormulaModel>> GetModelOrDefaultAsync(int id, int tenantId)
    {
        var result = new ServiceResult<FormulaModel>();

        // Fetch object
        var formula = await _unitOfWork.FormulaRepository.GetAsync(id, tenantId);

        // Set response
        if (formula == null)
        {
            result.SetNotFound($"Unable to locate Formula object ({id})");
            return result;
        }

        result.Data = _mapper.Map<FormulaModel>(formula);

        return result;
    }

    public async Task<ServiceResult<FormulaModel>> CreateAsync(string name, int categoryId, string description, IEnumerable<FormulaIngredientRequest> ingredients, UserProfile user)
    {
        var result = new ServiceResult<FormulaModel>();

        if (!await _categoryAuthorizationProvider.TenantHasResourceAccessAsync(user.TenantId.Value, categoryId))
        {
            result.SetNotFound($"CategoryId [{categoryId}] is invalid");
            return result;
        }

        foreach (var ingredient in ingredients)
        {
            // Ensure MaterialId belongs to Tenant
            if (!await _materialAuthorizationProvider.TenantHasResourceAccessAsync(user.TenantId.Value, ingredient.MaterialId.Value))
            {
                result.SetNotFound($"MaterialId [{ingredient.MaterialId}] is invalid");
                return result;
            }
        }

        var now = DateTime.UtcNow;

        // Build and add the new object
        var formula = new Formula
        {
            Name = name,
            CategoryId = categoryId,
            Description = description,
            CreatedUserId = user.Id,
            LastModifiedUserId = user.Id,
            TenantId = user.TenantId.Value,
            CreatedUtc = now,
            LastModifiedUtc = now
        };
        await _unitOfWork.FormulaRepository.AddAsync(formula);

        // Add Ingredients if included
        foreach (var ingredient in ingredients)
        {
            var formulaIngredient = new FormulaIngredient
            {
                Formula = formula,
                MaterialId = ingredient.MaterialId.Value,
                Quantity = ingredient.Quantity.Value,
                CreatedUserId = user.Id,
                LastModifiedUserId = user.Id,
                CreatedUtc = now,
                LastModifiedUtc = now
            };

            formula.Ingredients.Add(formulaIngredient);
        }

        // Set response
        if (await _unitOfWork.CompleteAsync() <= 0)
        {
            result.SetError("Failed to create Formula");
            return result;
        }

        var formulaResult = await GetModelOrDefaultAsync(formula.Id, user.TenantId.Value);
        result.Data = formulaResult.Data;

        return result;
    }

    public async Task<ServiceResult> UpdateAsync(int id, string name, int categoryId, string description, UserProfile user)
    {
        var result = new ServiceResult();

        if (!await _categoryAuthorizationProvider.TenantHasResourceAccessAsync(user.TenantId.Value, categoryId))
        {
            result.SetNotFound($"CategoryId [{categoryId}] is invalid");
            return result;
        }

        // Fetch the existing object
        var formula = await GetEntityOrDefaultAsync(id, user.TenantId.Value);
        if (formula == null)
        {
            result.SetNotFound($"Unable to locate Formula object ({id})");
            return result;
        }

        // Update properties
        formula.Name = name;
        formula.CategoryId = categoryId;
        formula.Description = description;
        formula.LastModifiedUserId = user.Id;
        formula.LastModifiedUtc = DateTime.UtcNow;

        // Set response
        if (await _unitOfWork.CompleteAsync() <= 0) result.SetError("Failed to update Formula");

        return result;
    }

    public async Task<ServiceResult> DeleteAsync(int id, UserProfile user)
    {
        var result = new ServiceResult();

        // Fetch the existing object
        var formula = await GetEntityOrDefaultAsync(id, user.TenantId.Value);
        if (formula == null)
        {
            result.SetNotFound($"Unable to locate Formula object ({id})");
            return result;
        }

        _unitOfWork.FormulaRepository.Remove(formula);

        if (await _unitOfWork.CompleteAsync() <= 0) result.SetError("Failed to delete Formula");

        return result;
    }

    private async Task<Formula> GetEntityOrDefaultAsync(int id, int tenantId)
    {
        // Fetch object
        return await _unitOfWork.FormulaRepository.GetAsync(id, tenantId);
    }
}
