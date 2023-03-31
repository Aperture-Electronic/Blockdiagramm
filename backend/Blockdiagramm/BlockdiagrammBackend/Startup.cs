using BlockdiagrammBackend.Models.Globals;
using BlockdiagrammBackend.Models.Project;
using BlockdiagrammBackend.Models.Server;
using BlockdiagrammBackend.Session;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Text.Json;

namespace BlockdiagrammBackend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options => options.AddDefaultPolicy(builder => builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")));

            AddCustomServices(services);
        }

        private void AddCustomServices(IServiceCollection services)
        {
            // Check the global cancellation token source for thread creating
            if (Globals.AppCancellationTokenSource == null)
            {
                throw new Exception("The global appliaction cancellation token source is not assigned");
            }

            // Configure server monitor
            ServerMonitor serverMonitor = new ServerMonitor(Globals.AppCancellationTokenSource);

            services.AddSingleton(serverMonitor);

            TimeSpan sessionTimeout = TimeSpan.FromDays(30);

            // Configure project manager
            SessionStorage<ProjectInstance> projectInstance = new(sessionTimeout, Globals.AppCancellationTokenSource);
            services.AddSingleton(projectInstance);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure web server
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints => endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}"
                ));
        }
    }
}