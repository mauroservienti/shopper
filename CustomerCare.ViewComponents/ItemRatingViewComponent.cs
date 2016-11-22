using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCare.ViewComponents
{
    [ViewComponent(Name = "CustomerCare.ViewComponents.ItemRating")]
    public class ItemRatingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic product)
        {
            return View(product.ItemRating);
        }
    }
}
