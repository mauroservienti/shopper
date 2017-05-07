using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shipping.ViewComponents
{
    [ViewComponent(Name = "Shipping.ViewComponents.ItemShippingInfo")]
    public class ItemShippingInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic product)
        {
            return View(product.ItemShippingInfo);
        }
    }
}