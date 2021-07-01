using Api.Models.MapProfiles;
using Api.Services;
using Api.Validation.Validators;
using Data;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInventoryUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, InventoryUnitOfWork>();
        }

        public static void AddInventoryApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<AuthenticationDetailService>();

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
            services.AddTransient<UserPostValidator>();
            services.AddTransient<MaterialRequestValidator>();
            services.AddTransient<RegisterUserPostValidator>();
            services.AddTransient<RegisterCompanyPostValidator>();
            services.AddTransient<TenantPostValidator>();
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
