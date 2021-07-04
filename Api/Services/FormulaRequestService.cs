using Api.Authorization;
using Api.Handlers;
using Api.Models.Dto;
using Api.Models.RequestModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class FormulaRequestService
    {
        private readonly ILogger<FormulaRequestService> _logger;
        private readonly FormulaEntityService _formulaEntityService;
        private readonly ResourceAuthorization<MaterialAuthorizationProvider> _materialAuthorizationProvider;
        private readonly ResourceAuthorization<CategoryAuthorizationProvider> _categoryAuthorizationProvider;

        public FormulaRequestService(
            ILogger<FormulaRequestService> logger,
            FormulaEntityService formulaEntityService,
            ResourceAuthorization<MaterialAuthorizationProvider> materialAuthorizationProvider,
            ResourceAuthorization<CategoryAuthorizationProvider> categoryAuthorizationProvider)
        {
            _logger = logger;
            _formulaEntityService = formulaEntityService;
            _materialAuthorizationProvider = materialAuthorizationProvider;
            _categoryAuthorizationProvider = categoryAuthorizationProvider;
        }

        public async Task<ResponseHandler<IEnumerable<FormulaModel>>> ProcessListRequestAsync(int tenantId)
        {
            var response = new ResponseHandler<IEnumerable<FormulaModel>>();

            response.Data = await _formulaEntityService.ListAsync(tenantId);

            return response;
        }

        public async Task<ResponseHandler<FormulaModel>> ProcessGetRequestAsync(int id, int tenantId)
        {
            var response = new ResponseHandler<FormulaModel>();

            response.Data = await _formulaEntityService.GetModelOrDefaultAsync(id, tenantId);
            if (response.Data == null)
            {
                response.SetNotFound($"Unable to locate Formula object ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler<FormulaModel>> ProcessCreateRequestAsync(FormulaRequest model, int createdByUserId, int tenantId)
        {
            var response = new ResponseHandler<FormulaModel>();

            if (!await _categoryAuthorizationProvider.TenantHasResourceAccessAsync(tenantId, model.CategoryId.Value))
            {
                response.SetNotFound($"CategoryId [{model.CategoryId}] is invalid");
                return response;
            }

            foreach (var ingredient in model.Ingredients)
            {
                // Ensure MaterialId belongs to Tenant
                if (!await _materialAuthorizationProvider.TenantHasResourceAccessAsync(tenantId, ingredient.MaterialId.Value))
                {
                    _logger.LogError("FormulaRequestService.ProcessCreateRequestAsync - Failed due to MaterialId included in Ingredients collection. Tenant does not have access or MaterialId is invalid, MaterialId: [{MaterialId}]", ingredient.MaterialId);

                    response.SetNotFound($"MaterialId [{ingredient.MaterialId}] is invalid");
                    return response;
                }
            }

            response.Data = await _formulaEntityService.CreateAsync(model, createdByUserId, tenantId);
            if (response.Data == null)
            {
                response.SetError("An unexpected error occurred while saving the Formula object");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessUpdateRequestAsync(int id, FormulaRequest model, int modifiedByUserId, int tenantId)
        {
            var response = new ResponseHandler();

            if (!await _categoryAuthorizationProvider.TenantHasResourceAccessAsync(tenantId, model.CategoryId.Value))
            {
                response.SetNotFound($"CategoryId [{model.CategoryId}] is invalid");
                return response;
            }

            // Fetch the existing object
            var formula = await _formulaEntityService.GetEntityOrDefaultAsync(id, tenantId);
            if (formula != null)
            {
                if (!await _formulaEntityService.UpdateAsync(formula, model, modifiedByUserId))
                {
                    response.SetError("An unexpected error occurred while saving the Formula object");
                }
            }
            else
            {
                response.SetNotFound($"Unable to locate Formula object ({id})");
            }

            return response;
        }

        public async Task<ResponseHandler> ProcessDeleteRequestAsync(int id, int deletedByUserId, int tenantId)
        {
            var response = new ResponseHandler();

            // Fetch the existing object
            var formula = await _formulaEntityService.GetEntityOrDefaultAsync(id, tenantId);
            if (formula != null)
            {
                if (!await _formulaEntityService.DeleteAsync(formula, deletedByUserId))
                {
                    response.SetError("An unexpected error occurred while removing the Formula object");
                }
            }
            else
            {
                response.SetNotFound($"Unable to locate Formula object ({id})");
            }

            return response;
        }
    }
}
