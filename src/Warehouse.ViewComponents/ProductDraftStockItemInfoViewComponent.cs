using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.ViewComponents
{
    [ViewComponent(Name = "Warehouse.ViewComponents.ProductDraftStockItemInfo")]
    public class ProductDraftStockItemInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model.StockItemInfo);
        }
    }
}
