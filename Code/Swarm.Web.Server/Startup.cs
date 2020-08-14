using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using Swarm.EntityFramework;

namespace Swarm.Web.Server
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
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       .AllowCredentials();
            }));

            services.AddMvc(
                options =>
                {
                    options.Filters.Add(typeof(UnitOfWork));
                }).AddNewtonsoftJson();

            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();

                options.AddLogging();
            });

            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))
                    .EnableSensitiveDataLogging());

            services.AddSpaStaticFiles(
                option => { option.RootPath = "wwwroot/"; });

            services.AddSignalR();
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
                endpoints.MapHub<GameHub>("/gamehub");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../Swarm.Web.Client";
                if (env.IsDevelopment())
                    spa.UseReactDevelopmentServer(npmScript: "start");
            });

            container.Verify();
        }

    }
}
