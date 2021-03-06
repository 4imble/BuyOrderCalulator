using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using BuyOrderCalc.EntityFramework;
using BuyOrderCalc.Web.Server.Helpers;

namespace BuyOrderCalc.Web.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private Container container;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            container = ApplicationContext.Container;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
                options =>
                {
                    options.Filters.Add(typeof(UnitOfWork));
                }).AddNewtonsoftJson();

            services.AddControllers();

            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();

                options.AddLogging();
            });

            container.Register<AuthHelper>();

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .EnableSensitiveDataLogging());

            services.AddSpaStaticFiles(
                option => { option.RootPath = "wwwroot/"; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(container);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../BuyOrderCalulator.Web.Client";
                if (env.IsDevelopment())
                    spa.UseReactDevelopmentServer(npmScript: "start");
            });

            container.Verify();
        }

    }
}
