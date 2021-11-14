using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Models.Results;
using AutoMapper;
using Data;
using Data.Models;
using System;
using System.Threading.Tasks;

namespace Api.Services;

public class UserEntityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserEntityService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ServiceResult<UserModel>> RegisterNewAsync(RegisterUserRequest model)
    {
        var response = new ServiceResult<UserModel>();

        var now = DateTime.UtcNow;

        // Build and add the new object
        var user = new UserProfile
        {
            LocalId = model.LocalId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            CreatedUtc = now,
            LastModifiedUtc = now
        };
        await _unitOfWork.UserRepository.AddAsync(user);

        // Set response
        if (await _unitOfWork.CompleteAsync() <= 0)
        {
            response.SetError("An unexpected error occurred while saving the User object");
            return response;
        }

        response.Data = _mapper.Map<UserModel>(user);

        return response;
    }
}
