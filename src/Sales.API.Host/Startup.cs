using Newtonsoft.Json.Serialization;
using Owin;
using Radical.Bootstrapper;
using Radical.Bootstrapper.Windsor.WebAPI.Infrastructure;
using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin.Cors;
using NServiceBus;
using Sales.Data.Migrations;
using Raven.Client;
using Castle.MicroKernel.Registration;

namespace Sales.API.Host
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            var bootstrapper = new WindsorBootstrapper(AppDomain.CurrentDomain.BaseDirectory, filter: "Sales*.*");
            var container = bootstrapper.Boot();

            var store = CommonConfiguration.CreateEmbeddableDocumentStore("Sales", session =>
            {
                SeedData.ItemPrices().ForEach(s => session.Store(s));
            });

            container.Register(Component.For<IDocumentStore>().Instance(store).LifestyleSingleton());

            var endpointConfiguration = new EndpointConfiguration("Sales");
            endpointConfiguration.UseContainer<WindsorBuilder>(c => c.ExistingContainer(container));

            endpointConfiguration.ApplyCommonConfiguration();
            endpointConfiguration.UseRavenPersistence(store);

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
