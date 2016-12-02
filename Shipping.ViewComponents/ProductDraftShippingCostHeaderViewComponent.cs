using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shipping.ViewComponents
{
    [ViewComponent(Name = "Shipping.ViewComponents.ProductDraftShippingCostHeader")]
    public class ProductDraftShippingCostHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View();
        }
    }
}
