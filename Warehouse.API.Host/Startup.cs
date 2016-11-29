using Castle.MicroKernel.Registration;
using Warehouse.Data.Context;
using Newtonsoft.Json.Serialization;
using Owin;
using Radical.Bootstrapper;
using Radical.Bootstrapper.Windsor.WebAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Cors;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using NServiceBus;
using NServiceBus.Persistence;
using System.Configuration;

namespace Warehouse.API.Host
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            var bootstrapper = new WindsorBootstrapper(AppDomain.CurrentDomain.BaseDirectory, filter: "*.*");
            var container = bootstrapper.Boot();

            var endpointConfiguration = new EndpointConfiguration("Warehouse");
            endpointConfiguration.UseContainer<WindsorBuilder>(c => c.ExistingContainer(container));

            endpointConfiguration.ApplyCommonConfiguration();
            endpointConfiguration.UseSqlitePersistence();

            var endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
            container.Register(Component.For<IMessageSession>().Instance(endpoint));

            var config = new HttpConfiguration();

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.DependencyResolver = new WindsorDependencyResolver(container);

            config.Formatters
                .JsonFormatter
                .SerializerSettings
                .ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.UseWebApi(config);
        }
    }
}
