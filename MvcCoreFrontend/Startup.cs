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

            RegisterSingletons(services, Configuration.GetSection("composers"));
            RegisterSingletons(services, Configuration.GetSection("services"));

            //Get a reference to the assembly that contains the view components
            var assembly = typeof(Sales.ViewComponents.HomeItemPriceViewComponent).GetTypeInfo().Assembly;

            //Add the file provider to the Razor view engine
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Add(new EmbeddedFileProvider(
                    assembly,
                    "Sales.ViewComponents"
                ));
            });
        }

        void RegisterSingletons(IServiceCollection services, IConfigurationSection section)
        {
            foreach (var item in section.GetChildren())
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
