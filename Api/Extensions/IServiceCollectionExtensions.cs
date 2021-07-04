﻿using Api.Authorization;
using Api.Models.MapProfiles;
using Api.Services;
using Api.Validation.Validators;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInventoryUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, InventoryUnitOfWork>();
        }

        public static void AddInventoryAuthorizationServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, IsAdminHandler>();

            services.AddTransient<AuthenticationDetailService>();

            services.AddTransient<ResourceAuthorization<CategoryAuthorizationProvider>>();
            services.AddTransient<CategoryAuthorizationProvider>();
            services.AddTransient<ResourceAuthorization<FormulaAuthorizationProvider>>();
            services.AddTransient<FormulaAuthorizationProvider>();
            services.AddTransient<ResourceAuthorization<MaterialAuthorizationProvider>>();
            services.AddTransient<MaterialAuthorizationProvider>();
        }

        public static void AddInventoryApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<CategoryEntityService>();
            services.AddTransient<CategoryRequestService>();

            services.AddTransient<CountryEntityService>();
            services.AddTransient<CountryRequestService>();

            services.AddTransient<FormulaEntityService>();
            services.AddTransient<FormulaRequestService>();

            services.AddTransient<MaterialEntityService>();
            services.AddTransient<MaterialInventoryTransactionService>();
            services.AddTransient<MaterialRequestService>();
            services.AddTransient<MaterialTransactionRequestService>();

            services.AddTransient<ProductEntityService>();
            services.AddTransient<ProductRequestService>();

            services.AddTransient<ProvinceEntityService>();
            services.AddTransient<ProvinceRequestService>();

            services.AddTransient<RegisterUserRequestService>();
            services.AddTransient<RegisterCompanyRequestService>();

            services.AddTransient<TenantEntityService>();
            services.AddTransient<TenantRequestService>();

            services.AddTransient<UnitOfMeasurementEntityService>();
            services.AddTransient<UnitOfMeasurementRequestService>();

            services.AddTransient<UserEntityService>();
            services.AddTransient<UserRequestService>();
        }

        public static void AddInventoryValidators(this IServiceCollection services)
        {
            services.AddTransient<FormulaRequestValidator>();
            services.AddTransient<UserRequestValidator>();
            services.AddTransient<MaterialRequestValidator>();
            services.AddTransient<RegisterUserRequestValidator>();
            services.AddTransient<RegisterCompanyRequestValidator>();
            services.AddTransient<TenantRequestValidator>();
        }

        public static void AddInventoryProfilesForAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(AddressProfile),
                typeof(CategoryProfile),
                typeof(CountryProfile),
                typeof(FormulaProfile),
                typeof(MaterialProfile),
                typeof(ProductProfile),
                typeof(ProvinceProfile),
                typeof(TenantProfile),
                typeof(UnitOfMeasurementProfile),
                typeof(UserProfile));
        }
    }
}
