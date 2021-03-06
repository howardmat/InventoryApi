using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Services;
using Api.Validation.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers;

[Route("/register/userprofile")]
[ApiController]
public class RegisterUserController : InventoryControllerBase
{
    private readonly RegisterUserRequestValidator _registerPostValidator;
    private readonly UserEntityService _userEntityService;

    public RegisterUserController(
        UserEntityService userEntityService,
        RegisterUserRequestValidator registerPostValidator)
    {
        _userEntityService = userEntityService;
        _registerPostValidator = registerPostValidator;
    }

    [HttpPost]
    public async Task<ActionResult<UserModel>> Post(RegisterUserRequest model)
    {
        if (!await _registerPostValidator.IsValidAsync(model))
            return _registerPostValidator.ServiceResponse.ToActionResult();

        var result = await _userEntityService.RegisterNewAsync(model);
        return result.ToActionResult(
            Url.Action("Get", "User", new { id = result.Data?.Id }));
    }
}
