using Api.Services;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers;

public class InventoryControllerBase : ControllerBase
{
    private readonly AuthenticationDetailService _authenticationDetailService;

    public InventoryControllerBase() { }

    public InventoryControllerBase(
        AuthenticationDetailService authenticationDetailService)
    {
        _authenticationDetailService = authenticationDetailService;
    }

    protected async Task<UserProfile> GetCurrentUserAsync(ClaimsPrincipal principal)
    {
        if (_authenticationDetailService == null) throw new Exception("AuthenticationDetailService is required in base constructor for GetCurrentUserAsync to be called in child class.");

        return await _authenticationDetailService.GetUserBasedOnClaimsAsync(principal);
    }
}
