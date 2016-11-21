using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.ViewComponents
{
    [ViewComponent(Name = "Sales.ViewComponents.HomeItemPrice")]
    public class HomeItemPriceViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic product)
        {
            return View(product.ItemPrice);
        }
    }
}
