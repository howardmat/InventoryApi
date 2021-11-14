using Api.Authorization;
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

            services.AddTransient<ResourceAuthorization<ProductAuthorizationProvider>>();
            services.AddTransient<ProductAuthorizationProvider>();
        }

        public static void AddInventoryApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<CategoryEntityService>();
            services.AddTransient<CountryEntityService>();
            services.AddTransient<FormulaEntityService>();
            services.AddTransient<FormulaIngredientEntityService>();
            services.AddTransient<MaterialEntityService>();
            services.AddTransient<MaterialInventoryTransactionService>();
            services.AddTransient<ProductEntityService>();
            services.AddTransient<ProductInventoryTransactionService>();
            services.AddTransient<ProvinceEntityService>();
            services.AddTransient<TenantEntityService>();
            services.AddTransient<UnitOfMeasurementEntityService>();
            services.AddTransient<UserEntityService>();
        }

        public static void AddInventoryValidators(this IServiceCollection services)
        {
            services.AddTransient<FormulaIngredientRequestValidator>();
            services.AddTransient<FormulaRequestValidator>();
            services.AddTransient<MaterialRequestValidator>();
            services.AddTransient<ProductRequestValidator>();
            services.AddTransient<RegisterCompanyRequestValidator>();
            services.AddTransient<RegisterUserRequestValidator>();
            services.AddTransient<TenantRequestValidator>();
            services.AddTransient<UserRequestValidator>();
        }

        public static void AddInventoryProfilesForAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(AddressProfile),
                typeof(CategoryProfile),
                typeof(CountryProfile),
                typeof(FormulaIngredientProfile),
                typeof(FormulaProfile),
                typeof(MaterialInventoryTransactionProfile),
                typeof(MaterialProfile),
                typeof(ProductInventoryTransactionProfile),
                typeof(ProductProfile),
                typeof(ProvinceProfile),
                typeof(TenantProfile),
                typeof(UnitOfMeasurementProfile),
                typeof(UserProfile));
        }
    }
}
