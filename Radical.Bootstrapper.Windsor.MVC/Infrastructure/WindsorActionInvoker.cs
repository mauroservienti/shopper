﻿using System.Collections.Generic;
using System.Web.Mvc;
using Castle.MicroKernel;
using System.Web.Mvc.Async;

namespace Radical.Bootstrapper.Windsor.MVC.Infrastructure
{
    public class WindsorActionInvoker : AsyncControllerActionInvoker
    {
        readonly IKernel kernel;

        public WindsorActionInvoker( IKernel kernel )
        {
            this.kernel = kernel;
        }

        void Append<TFilterType>( IList<TFilterType> bag )
        {
            var all = this.kernel.ResolveAll<TFilterType>();
            foreach ( var a in all )
            {
                bag.Add( a );
            }
        }

        protected override FilterInfo GetFilters( ControllerContext controllerContext, ActionDescriptor actionDescriptor )
        {
            var info = base.GetFilters( controllerContext, actionDescriptor );

            this.Append<IAuthorizationFilter>( info.AuthorizationFilters );
            this.Append<IActionFilter>( info.ActionFilters );
            this.Append<IResultFilter>( info.ResultFilters );
            this.Append<IExceptionFilter>( info.ExceptionFilters );

            return info;
        }
    }
}