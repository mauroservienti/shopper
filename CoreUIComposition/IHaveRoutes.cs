using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace CoreUIComposition
{
    public interface IHaveRoutes
    {
        void BuildRoutes(IRouteBuilder routes);
    }
}
