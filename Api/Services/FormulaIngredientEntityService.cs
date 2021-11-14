using Api.Authorization;
using Api.Models.Dto;
using Api.Models.Results;
using AutoMapper;
using Data;
using Data.Models;
using System;
using System.Threading.Tasks;

namespace Api.Services
{
    public class FormulaIngredientEntityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResourceAuthorization<MaterialAuthorizationProvider> _materialAuthorizationProvider;
        private readonly ResourceAuthorization<FormulaAuthorizationProvider> _formulaAuthorizationProvider;

        public FormulaIngredientEntityService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ResourceAuthorization<FormulaAuthorizationProvider> formulaAuthorizationProvider,
            ResourceAuthorization<MaterialAuthorizationProvider> materialAuthorizationProvider)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _materialAuthorizationProvider = materialAuthorizationProvider;
            _formulaAuthorizationProvider = formulaAuthorizationProvider;
        }

        private async Task<FormulaIngredient> GetEntityOrDefaultAsync(int id, int tenantId)
        {
            // Fetch object
            return await _unitOfWork.FormulaIngredientRepository.GetAsync(id, tenantId);
        }

        public async Task<ServiceResult<FormulaIngredientModel>> CreateAsync(int formulaId, int materialId, decimal quantity, UserProfile user)
        {
            var result = new ServiceResult<FormulaIngredientModel>();

            if (!await _materialAuthorizationProvider.TenantHasResourceAccessAsync(user.TenantId.Value, materialId))
            {
                result.SetNotFound($"MaterialId [{materialId}] is invalid");
                return result;
            }

            if (!await _formulaAuthorizationProvider.TenantHasResourceAccessAsync(user.TenantId.Value, formulaId))
            {
                result.SetNotFound($"FormulaId [{formulaId}] is invalid");
                return result;
            }

            var now = DateTime.UtcNow;

            // Build and add the new object
            var formulaIngredient = new FormulaIngredient
            {
                FormulaId = formulaId,
                MaterialId = materialId,
                Quantity = quantity,
                CreatedUserId = user.Id,
                CreatedUtc = now,
                LastModifiedUserId = user.Id,
                LastModifiedUtc = now
            };
            await _unitOfWork.FormulaIngredientRepository.AddAsync(formulaIngredient);

            // Set response
            if (await _unitOfWork.CompleteAsync() <= 0)
            {
                result.SetError($"An unexpected error occurred while saving the Formula Ingredient object");
                return result;
            }

            result.Data = _mapper.Map<FormulaIngredientModel>(formulaIngredient);

            return result;
        }

        public async Task<ServiceResult> DeleteAsync(int id, UserProfile user)
        {
            var result = new ServiceResult();

            // Fetch the existing object
            var formulaIngredient = await GetEntityOrDefaultAsync(id, user.TenantId.Value);
            if (formulaIngredient == null)
            {
                result.SetNotFound($"Unable to locate Formula Ingredient object ({id})");
                return result;
            }

            _unitOfWork.FormulaIngredientRepository.Remove(formulaIngredient);

            if (await _unitOfWork.CompleteAsync() <= 0)
            {
                result.SetError($"An unexpected error occurred while removing the Formula Ingredient object");
            }

            return result;
        }
    }
}
