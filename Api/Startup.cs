using Api.Models.MapProfiles;
using Api.Services;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            AddAutoMapperWithProfiles(services);

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, InventoryUnitOfWork>();

            services.AddTransient<CategoryEntityService>();
            services.AddTransient<CategoryRequestService>();
            services.AddTransient<CountryEntityService>();
            services.AddTransient<CountryRequestService>();
            services.AddTransient<MaterialEntityService>();
            services.AddTransient<MaterialRequestService>();
            services.AddTransient<ProvinceEntityService>();
            services.AddTransient<ProvinceRequestService>();
            services.AddTransient<TenantEntityService>();
            services.AddTransient<TenantRequestService>();
            services.AddTransient<UnitOfMeasurementService>();
            services.AddTransient<UserService>();
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
