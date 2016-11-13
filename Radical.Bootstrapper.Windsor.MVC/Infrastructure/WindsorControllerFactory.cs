using System;
using System.Web;
using System.Web.Mvc;
using Castle.Windsor;
using System.Web.Routing;

namespace Radical.Bootstrapper.Windsor.MVC.Infrastructure
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        readonly IWindsorContainer kernel;

        public WindsorControllerFactory( IWindsorContainer kernel )
        {
            this.kernel = kernel;
        }

        public override void ReleaseController( IController controller )
        {
            kernel.Release( controller );
        }

        protected override IController GetControllerInstance( RequestContext requestContext, Type controllerType )
        {
            if ( controllerType == null )
            {
                throw new HttpException( 404, string.Format( "The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path ) );
            }

            var iController = ( IController )kernel.Resolve( controllerType );

            return iController;
        }
    }
}