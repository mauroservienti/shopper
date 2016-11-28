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
using CoreUIComposition;

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
        List<Type> routesBuilders= new List<Type>();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var modules = Configuration.GetSection("modules").GetChildren();

            var allServices = new List<IConfigurationSection>();
            var allViewComponents = new List<IConfigurationSection>();

            foreach (var module in modules)
            {
                var moduleName = module.Key;

                allServices.AddRange(
                    Configuration.GetSection($"modules:{moduleName}:services").GetChildren()
                    );

                allViewComponents.AddRange(
                    Configuration.GetSection($"modules:{moduleName}:viewComponents").GetChildren()
                    );

                var routesBuilder = Configuration.GetSection($"modules:{moduleName}:routesBuilder").Value;
                if (routesBuilder != null)
                {
                    routesBuilders.Add(Type.GetType(routesBuilder, true));
                }
            }

            var viewComponents = allViewComponents.Select(cs =>
            {
                var an = new AssemblyName(cs.Value);
                var a = Assembly.Load(an);

                return new
                {
                    baseNamesapce = cs.Value,
                    assembly = a
                };
            })
            .ToList();

            viewComponents.ForEach(vc => 
            {
                services.Configure<RazorViewEngineOptions>(options =>
                {
                    options.FileProviders.Add(new EmbeddedFileProvider(vc.assembly, vc.baseNamesapce));
                });
            });

            allServices.ForEach(item => 
            {
                var contract = Type.GetType(item.GetValue<string>("contract"));
                var implementation = Type.GetType(item.GetValue<string>("implementation"));
                services.AddSingleton(contract, implementation);
            });

            // Add framework services.
            var imvc = services.AddMvc();

            viewComponents.ForEach(vc =>
            {
                imvc = imvc.AddApplicationPart(vc.assembly);
            });

            imvc.AddControllersAsServices();

            services.AddSingleton<IConfiguration>(Configuration);
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
                app.UseExceptionHandler("/UnhandledError/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                foreach (var rbt in routesBuilders)
                {
                    var rb = (IHaveRoutes)Activator.CreateInstance(rbt);
                    rb.BuildRoutes(routes);
                }
            });
        }
    }
}
