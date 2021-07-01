using Api.Claims;
using Api.Extensions;
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
        private string _firebaseApiUrl;
        private string _firebaseProjectId;
        private string _secureTokenUrl;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _firebaseApiUrl = _configuration["Firebase:ApiUrl"];
            _firebaseProjectId = _configuration["Firebase:ProjectId"];
            _secureTokenUrl = $"{_firebaseApiUrl}/{_firebaseProjectId}";
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(
                    _configuration.GetConnectionString("DefaultConnection")));

            services.AddInventoryProfilesForAutoMapper();

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

            services.AddInventoryUnitOfWork();

            services.AddInventoryApplicationServices();

            services.AddInventoryValidators();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }
            else
            {
                app.UseExceptionHandler("/error");
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
    }
}
