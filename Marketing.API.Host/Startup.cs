using Castle.MicroKernel.Registration;
using Marketing.Data.Context;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using NServiceBus;
using Owin;
using Radical.Bootstrapper;
using Radical.Bootstrapper.Windsor.WebAPI.Infrastructure;
using System;
using System.ComponentModel.Composition.Hosting;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;

namespace Marketing.API.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var bootstrapper = new WindsorBootstrapper(AppDomain.CurrentDomain.BaseDirectory, filter: "*.*");
            var container = bootstrapper.Boot();

            var endpointConfiguration = new EndpointConfiguration("Marketing");
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
