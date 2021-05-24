﻿using Api.Extensions;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly TenantRequestService _tenantRequestService;

        public TenantController(
            TenantRequestService tenantRequestService)
        {
            _tenantRequestService = tenantRequestService;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TenantModel>> Get(int id)
        {
            // Get data from service
            var result = await _tenantRequestService.ProcessGetRequestAsync(id);
            return this.GetResultFromServiceResponse(result);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<TenantModel>> Post(TenantModel model)
        {
            // Get current user id
            var userId = this.GetCurrentUserId(User);

            // Create new record
            var result = await _tenantRequestService.ProcessCreateRequestAsync(model, userId);
            return this.GetResultFromServiceResponse(result,
                Url.Action("Get", "Tenant", new { id = result.Data?.Id }));
        }
    }
}
