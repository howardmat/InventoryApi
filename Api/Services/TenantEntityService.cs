using Api.Extensions;
using Api.Models;
using AutoMapper;
using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public class TenantEntityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TenantEntityService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TenantModel>> ListAsync()
        {
            // Fetch data
            var data = await _unitOfWork.TenantRepository.ListAsync();

            // Add to collection
            var list = new List<TenantModel>();
            foreach (var item in data)
            {
                list.Add(_mapper.Map<TenantModel>(item));
            }

            return list;
        }

        public async Task<Tenant> GetEntityOrDefaultAsync(int id)
        {
            // Fetch object
            var entity = await _unitOfWork.TenantRepository.GetAsync(id);

            return entity;
        }

        public async Task<TenantModel> GetModelOrDefaultAsync(int id)
        {
            TenantModel model = null;

            // Fetch object
            var tenant = await GetEntityOrDefaultAsync(id);
            if (tenant != null)
            {
                model = _mapper.Map<TenantModel>(tenant);
            }

            return model;
        }

        public async Task<TenantModel> CreateAsync(TenantModel model, int modifyingUserId, bool assignCreatorAsOwner = false)
        {
            TenantModel newModel = null;

            var now = DateTime.UtcNow;

            var tenantAddress = new Address(
                model.PrimaryAddress.StreetAddress,
                model.PrimaryAddress.City,
                model.PrimaryAddress.PostalCode.StripPostalCodeFormatting(),
                model.PrimaryAddress.CountryIsoCode,
                model.PrimaryAddress.ProvinceIsoCode,
                modifyingUserId);
            
            var tenant = new Tenant
            {
                CompanyName = model.CompanyName,
                OwnerUserId = modifyingUserId,
                PrimaryAddress = tenantAddress,
                CreatedUserId = modifyingUserId,
                LastModifiedUserId = modifyingUserId,
                CreatedUtc = now,
                LastModifiedUtc = now
            };
            await _unitOfWork.TenantRepository.AddAsync(tenant);

            if (assignCreatorAsOwner)
            {
                var ownerUser = await _unitOfWork.UserRepository.GetAsync(modifyingUserId);
                ownerUser.Tenant = tenant;
            }

            // Save data
            if (await _unitOfWork.CompleteAsync() > 0)
            {
                tenant = await _unitOfWork.TenantRepository.GetAsync(tenant.Id);

                newModel = _mapper.Map<TenantModel>(tenant);
            }

            return newModel;
        }

        public async Task<bool> UpdateAsync(Tenant tenant, TenantModel model, int modifyingUserId)
        {
            var now = DateTime.UtcNow;

            // Update properties
            tenant.CompanyName = model.CompanyName;
            tenant.LastModifiedUserId = modifyingUserId;
            tenant.LastModifiedUtc = now;

            if (tenant.PrimaryAddress == null)
            {
                tenant.PrimaryAddress = new Address(
                    model.PrimaryAddress.StreetAddress,
                    model.PrimaryAddress.City,
                    model.PrimaryAddress.PostalCode.StripPostalCodeFormatting(),
                    model.PrimaryAddress.CountryIsoCode,
                    model.PrimaryAddress.ProvinceIsoCode,
                    modifyingUserId);
            }
            else
            {
                tenant.PrimaryAddress.Update(
                    model.PrimaryAddress.StreetAddress, 
                    model.PrimaryAddress.City,
                    model.PrimaryAddress.PostalCode.StripPostalCodeFormatting(),
                    model.PrimaryAddress.CountryIsoCode,
                    model.PrimaryAddress.ProvinceIsoCode,
                    modifyingUserId);
            }            

            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }

        public async Task<bool> DeleteAsync(Tenant tenant, int modifyingUserId)
        {
            var now = DateTime.UtcNow;

            // Update entity
            tenant.DeletedUserId = modifyingUserId;
            tenant.DeletedUtc = now;

            var success = await _unitOfWork.CompleteAsync() > 0;

            return success;
        }
    }
}
