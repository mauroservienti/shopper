using Castle.MicroKernel.Registration;
using Newtonsoft.Json.Serialization;
using Owin;
using Radical.Bootstrapper;
using Radical.Bootstrapper.Windsor.WebAPI.Infrastructure;
using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin.Cors;
using System.Web.Http.Batch;
using Raven.Client;
using CustomerCare.Data.Migrations;
using NServiceBus;

namespace CustomerCare.API.Host
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            var bootstrapper = new WindsorBootstrapper(AppDomain.CurrentDomain.BaseDirectory, filter: "CustomerCare*.*");
            var container = bootstrapper.Boot();

            var store = CommonConfiguration.CreateEmbeddableDocumentStore("CustomerCare", session =>
            {
                SeedData.Raitings().ForEach(r => session.Store(r));
                SeedData.Reviews().ForEach(r => session.Store(r));
            });

            container.Register(Component.For<IDocumentStore>().Instance(store).LifestyleSingleton());

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

            HttpServer server = new HttpServer(config);

            config.Routes.MapHttpBatchRoute(
                routeName: "batch",
                routeTemplate: "api/batch",
                batchHandler: new DefaultHttpBatchHandler(server)
            );

            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.UseWebApi(config);
        }
    }
}
