using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketing.ViewComponents
{
    [ViewComponent(Name = "Marketing.ViewComponents.MarketingMenuItem")]
    public class MarketingMenuItemViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model);
        }
    }
}
