using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketing.ViewComponents
{
    [ViewComponent(Name = "Marketing.ViewComponents.HomeShowcaseItemDescription")]
    public class HomeShowcaseItemDescriptionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic product)
        {
            return View(product.ItemDescription);
        }
    }
}
