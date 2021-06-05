using Api.Claims;
using Api.Models.MapProfiles;
using Api.Services;
using Api.Validation.Validators;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Startup
    {
        private IConfiguration _configuration;
        private string _firebaseProjectId;
        private string _secureTokenUrl;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _firebaseProjectId = _configuration["Firebase:ProjectId"];
            _secureTokenUrl = $"https://securetoken.google.com/{_firebaseProjectId}";
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(
                    _configuration.GetConnectionString("DefaultConnection")));

            AddAutoMapperWithProfiles(services);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = _secureTokenUrl;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _secureTokenUrl,
                        ValidateAudience = true,
                        ValidAudience = _firebaseProjectId,
                        ValidateLifetime = true
                    };
                });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });

            AddApplicationServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseInjectTenantIdClaim();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, InventoryUnitOfWork>();

            services.AddTransient<AuthenticationDetailService>();
            services.AddTransient<CategoryEntityService>();
            services.AddTransient<CategoryRequestService>();
            services.AddTransient<CountryEntityService>();
            services.AddTransient<CountryRequestService>();
            services.AddTransient<MaterialEntityService>();
            services.AddTransient<MaterialRequestService>();
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

            services.AddTransient<UserPostValidator>();
            services.AddTransient<RegisterUserPostValidator>();
            services.AddTransient<RegisterCompanyPostValidator>();
            services.AddTransient<TenantPostValidator>();
        }

        private void AddAutoMapperWithProfiles(IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(AddressProfile),
                typeof(CategoryProfile),
                typeof(CountryProfile),
                typeof(MaterialProfile),
                typeof(ProvinceProfile),
                typeof(TenantProfile),
                typeof(UnitOfMeasurementProfile),
                typeof(UserProfile));
        }
    }
}
