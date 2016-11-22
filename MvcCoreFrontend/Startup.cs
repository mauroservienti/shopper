using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CoreViewModelComposition;
using System.Reflection;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Mvc.Razor;

namespace MvcCoreFrontend
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSingleton<IConfiguration>(Configuration);

            var modules = Configuration.GetSection("modules").GetChildren();

            var allServices = new List<IConfigurationSection>();
            var allComposers = new List<IConfigurationSection>();
            var allViewComponents = new List<IConfigurationSection>();

            foreach (var module in modules)
            {
                var moduleName = module.Key;

                allServices.AddRange(
                    Configuration.GetSection($"modules:{moduleName}:services").GetChildren()
                    );

                allComposers.AddRange(
                    Configuration.GetSection($"modules:{moduleName}:composers").GetChildren()
                    );

                allViewComponents.AddRange(
                    Configuration.GetSection($"modules:{moduleName}:viewComponents").GetChildren()
                    );
            }

            RegisterSingletons(services, allServices);
            RegisterSingletons(services, allComposers);
            RegisterViewComponents(services, allViewComponents);
        }

        void RegisterViewComponents(IServiceCollection services, IEnumerable<IConfigurationSection> viewComponents)
        {
            foreach (var vc in viewComponents)
            {
                var an = new AssemblyName(vc.Value);
                var a = Assembly.Load(an);

                services.Configure<RazorViewEngineOptions>(options =>
                {
                    options.FileProviders.Add(new EmbeddedFileProvider(a, vc.Value));
                });
            }
        }

        void RegisterSingletons(IServiceCollection services, IEnumerable<IConfigurationSection> registrations)
        {
            foreach (var item in registrations)
            {
                var contract = Type.GetType(item.GetValue<string>("contract"));
                var implementation = Type.GetType(item.GetValue<string>("implementation"));
                services.AddSingleton(contract, implementation);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
