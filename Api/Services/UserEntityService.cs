using Api.Models.Dto;
using AutoMapper;
using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
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

        public async Task<IEnumerable<UserModel>> ListAsync()
        {
            // Fetch data
            var data = await _unitOfWork.UserRepository.ListAsync();

            // Add to collection
            var list = new List<UserModel>();
            foreach (var item in data)
            {
                list.Add(_mapper.Map<UserModel>(item));
            }

            return list;
        }

        public async Task<UserProfile> GetEntityOrDefaultAsync(int id)
        {
            // Fetch object
            var entity = await _unitOfWork.UserRepository.GetAsync(id);

            return entity;
        }

        public async Task<UserModel> GetModelOrDefaultAsync(int id)
        {
            UserModel model = null;

            // Fetch object
            var user = await GetEntityOrDefaultAsync(id);
            if (user != null)
            {
                model = _mapper.Map<UserModel>(user);
            }

            return model;
        }

        public async Task<UserModel> CreateAsync(string localId, string email, string firstName, string lastName, int? modifyingUserId = null)
        {
            UserModel model = null;

            var now = DateTime.UtcNow;

            // Build and add the new object
            var user = new UserProfile
            {
                LocalId = localId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                CreatedUserId = modifyingUserId,
                CreatedUtc = now,
                LastModifiedUserId = modifyingUserId,
                LastModifiedUtc = now
            };
            await _unitOfWork.UserRepository.AddAsync(user);

            // Set response
            if (await _unitOfWork.CompleteAsync() > 0)
            {
                model = _mapper.Map<UserModel>(user);
            }

            return model;
        }

        public async Task<bool> UpdateAsync(UserProfile user, UserModel userModel, int modifyingUserId)
        {
            var now = DateTime.UtcNow;

            // Update entity
            user.Email = userModel.Email;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.LastModifiedUserId = modifyingUserId;
            user.LastModifiedUtc = now;

            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }

        public async Task<bool> DeleteAsync(UserProfile user, int modifyingUserId)
        {
            var now = DateTime.UtcNow;

            // Update entity
            user.DeletedUserId = modifyingUserId;
            user.DeletedUtc = now;

            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }
    }
}
