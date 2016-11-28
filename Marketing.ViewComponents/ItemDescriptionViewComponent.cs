using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketing.ViewComponents
{
    [ViewComponent(Name = "Marketing.ViewComponents.ItemDescription")]
    public class ItemDescriptionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model.ItemDescription);
        }
    }
}
