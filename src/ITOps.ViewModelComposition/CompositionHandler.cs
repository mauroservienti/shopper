using ITOps.ViewModelComposition.Engine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITOps.ViewModelComposition.Gateway
{
    public class CompositionHandler
    {
        public static async Task<(dynamic ViewModel, int StatusCode)> HandleRequest(HttpContext context)
        {
            var pending = new List<Task>();
            var routeData = context.GetRouteData();
            var request = context.Request;
            var vm = new DynamicViewModel(routeData, request);
            var interceptors = context.RequestServices.GetServices<IInterceptRoutes>();

            try
            {
                //matching interceptors could be cached by URL
                var matching = interceptors
                    .Where(a => a.Matches(routeData, request.Method, request))
                    .ToArray();

                foreach (var subscriber in matching.OfType<ISubscribeToCompositionEvents>())
                {
                    subscriber.Subscribe(vm, routeData, request);
                }

                foreach (var handler in matching.OfType<IHandleRequests>())
                {
                    pending.Add
                    (
                        handler.Handle(vm, routeData, request)
                    );
                }

                if (pending.Count == 0)
                {
                    return (null, StatusCodes.Status404NotFound);
                }
                else
                {
                    await Task.WhenAll(pending);

                    //result transformer? e.g. to change from vm.OrdersViewModel to orders[]

                    return (vm, StatusCodes.Status200OK);
                }
            }
            finally
            {
                vm.CleanupSubscribers();
            }
        }
    }
}
