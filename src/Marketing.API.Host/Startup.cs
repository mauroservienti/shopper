using Castle.MicroKernel.Registration;
using Marketing.Data.Migrations;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using NServiceBus;
using Owin;
using Radical.Bootstrapper;
using Radical.Bootstrapper.Windsor.WebAPI.Infrastructure;
using Raven.Client;
using System;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Marketing.API.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var bootstrapper = new WindsorBootstrapper(AppDomain.CurrentDomain.BaseDirectory, filter: "Marketing*.*");
            var container = bootstrapper.Boot();

            var store = CommonConfiguration.CreateEmbeddableDocumentStore("Marketing", session =>
            {
                SeedData.Products().ForEach(s => session.Store(s));
                session.Store(SeedData.HomeStructure());
            });

            container.Register(Component.For<IDocumentStore>().Instance(store).LifestyleSingleton());

            var endpointConfiguration = new EndpointConfiguration("Marketing");
            endpointConfiguration.UseContainer<WindsorBuilder>(c => c.ExistingContainer(container));

            endpointConfiguration.ApplyCommonConfiguration();
            endpointConfiguration.UseRavenPersistence(store);
            endpointConfiguration.LimitMessageProcessingConcurrencyTo(1);

            //var timeoutManager = endpointConfiguration.TimeoutManager();
            //timeoutManager.LimitMessageProcessingConcurrencyTo(4);

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
