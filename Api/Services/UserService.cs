using Api.Models;
using AutoMapper;
using Data;
using Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class UserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(
            ILogger<UserService> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UserModel>> GetAsync(int id)
        {
            var response = new ServiceResponse<UserModel>();

            try
            {
                // Fetch object
                var user = await _unitOfWork.UserRepository.FindByIdAsync(id);

                // Set response
                if (user != null)
                {
                    response.Data = _mapper.Map<UserModel>(user);
                }
                else
                {
                    response.SetNotFound($"Unable to locate User object ({id})");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("UserService.GetAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }

        public async Task<ServiceResponse<UserModel>> CreateOrUpdateAsync(UserModel model, int modifyingUserId)
        {
            var response = new ServiceResponse<UserModel>();

            try
            {
                var now = DateTime.UtcNow;

                // Look for an existing user by account Id
                var user = await _unitOfWork.UserRepository.FindByIdAsync(model.Id.Value);
                if (user == null)
                {
                    // Create new user object
                    user = new User
                    {
                        CreatedUserId = modifyingUserId,
                        CreatedUtc = now
                    };
                    await _unitOfWork.UserRepository.AddAsync(user);
                }

                // Update existing properties
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.LastModifiedUserId = modifyingUserId;
                user.LastModifiedUtc = now;

                // Set response
                if (await _unitOfWork.CompleteAsync() > 0)
                {
                    response.Data = _mapper.Map<UserModel>(user);
                }
                else
                {
                    response.SetError($"An unexpected error occurred while saving the Category object");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("UserService.CreateOrUpdateAsync - exception:{@Exception}", ex);

                response.SetException();
            }

            return response;
        }
    }
}
