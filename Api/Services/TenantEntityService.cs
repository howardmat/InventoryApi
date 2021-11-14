using Api.Extensions;
using Api.Models.Dto;
using Api.Models.RequestModels;
using Api.Models.Results;
using AutoMapper;
using Data;
using Data.Models;
using System;
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

        public async Task<ServiceResult<TenantModel>> RegisterNewAsync(RegisterCompanyRequest model, UserProfile user)
        {
            var response = new ServiceResult<TenantModel>();

            var assignCreatorAsOwner = true;

            var now = DateTime.UtcNow;

            var tenantAddress = new Address(
                model.PrimaryAddress.StreetAddress,
                model.PrimaryAddress.City,
                model.PrimaryAddress.PostalCode.StripPostalCodeFormatting(),
                model.PrimaryAddress.CountryIsoCode,
                model.PrimaryAddress.ProvinceIsoCode,
                user.Id);

            var tenant = new Tenant
            {
                CompanyName = model.CompanyName,
                OwnerUserId = user.Id,
                PrimaryAddress = tenantAddress,
                CreatedUserId = user.Id,
                LastModifiedUserId = user.Id,
                CreatedUtc = now,
                LastModifiedUtc = now
            };
            await _unitOfWork.TenantRepository.AddAsync(tenant);

            if (assignCreatorAsOwner)
            {
                var ownerUser = await _unitOfWork.UserRepository.GetAsync(user.Id);
                ownerUser.Tenant = tenant;
            }

            // Save data
            if (await _unitOfWork.CompleteAsync() <= 0)
            {
                response.SetError("An unexpected error occurred while registering the Company");
                return response;
            }

            tenant = await _unitOfWork.TenantRepository.GetAsync(tenant.Id);

            response.Data = _mapper.Map<TenantModel>(tenant);

            return response;
        }
    }
}
