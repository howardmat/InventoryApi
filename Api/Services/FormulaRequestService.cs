using Api.Handlers;
using Api.Models.Dto;
using Api.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class FormulaRequestService
    {
        private readonly FormulaEntityService _formulaEntityService;
        public FormulaRequestService(
            FormulaEntityService formulaEntityService)
        {
            _formulaEntityService = formulaEntityService;
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
