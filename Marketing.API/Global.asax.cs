using Castle.MicroKernel.Registration;
using Radical.Bootstrapper;
using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Web.Http;
using Marketing.Data.Context;

namespace Marketing.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            var bootstrapper = new WindsorBootstrapper(Path.Combine(basePath, "bin"));
            var container = bootstrapper.Boot();

            var dataManagerComponent = Component.For<IMarketingContext>()
                .Instance(new MarketingContext())
                .LifestyleSingleton();

            container.Register(dataManagerComponent);

            GlobalConfiguration.Configure(http => WebApiConfig.Register(http, container));
        }
    }
}
