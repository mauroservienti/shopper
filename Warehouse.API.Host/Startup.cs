using Castle.MicroKernel.Registration;
using Newtonsoft.Json.Serialization;
using Owin;
using Radical.Bootstrapper;
using Radical.Bootstrapper.Windsor.WebAPI.Infrastructure;
using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin.Cors;
using NServiceBus;
using Raven.Client;
using Warehouse.Data.Migrations;
using Microsoft.Owin;
using System.Threading.Tasks;

namespace Warehouse.API.Host
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            var bootstrapper = new WindsorBootstrapper(AppDomain.CurrentDomain.BaseDirectory, filter: "Warehouse*.*");
            var container = bootstrapper.Boot();

            var store = CommonConfiguration.CreateEmbeddableDocumentStore("Warehouse", session =>
            {
                SeedData.StockItems().ForEach(s => session.Store(s));
                session.Store(new { Id = "Raven/Hilo/StockItems", Max = 100 });
            });

            container.Register(Component.For<IDocumentStore>().Instance(store).LifestyleSingleton());

            var endpointConfiguration = new EndpointConfiguration("Warehouse");
            endpointConfiguration.UseContainer<WindsorBuilder>(c => c.ExistingContainer(container));

            endpointConfiguration.ApplyCommonConfiguration();
            endpointConfiguration.UseRavenPersistence(store);

            var endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
            container.Register(Component.For<IMessageSession>().Instance(endpoint));

            var config = new HttpConfiguration();

            appBuilder.Use<GlobalExceptionMiddleware>();

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

    public class GlobalExceptionMiddleware : OwinMiddleware
    {
        public GlobalExceptionMiddleware(OwinMiddleware next) : base(next)
        { }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception ex)
            {
                // your handling logic
                throw;
            }
        }
    }
}
