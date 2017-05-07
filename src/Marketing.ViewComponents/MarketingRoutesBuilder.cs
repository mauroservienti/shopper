using CoreUIComposition;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace Marketing.ViewComponents
{
    public class MarketingRoutesBuilder : IHaveRoutes
    {
        public void BuildRoutes(IRouteBuilder routes)
        {
            //routes.MapRoute(
            //        name: "products",
            //        template: "Products/{id:int}",
            //        defaults: new { controller = "Products", action = "Details" });
        }
    }
}
