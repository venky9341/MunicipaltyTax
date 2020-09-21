using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Municipality.Tax.Calculator.Models;
using Municipality.Tax.Calculator.Repository;

namespace Municipality.Tax.Calculator.API
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
            services.AddControllers();

            services.AddDbContext<DbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MunicipalityTaxCalculator")));
            services.AddScoped<IGenericRepository<MunicipaltyTax>, MunicipaltyTaxRepository<MunicipaltyTax>>();
            services.AddScoped<ITaxScheduleRepository<TaxSchedule>, TaxScheduleRepository<TaxSchedule>>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
