using Radical.Bootstrapper;
using Radical.Bootstrapper.Windsor.MVC.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Client.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var bootstrapper = new WindsorBootstrapper(
                directory: Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "bin"),
                filter: "*.*");
            var container = bootstrapper.Boot();

            ControllerBuilder.Current.SetControllerFactory(container.Resolve<IControllerFactory>());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
