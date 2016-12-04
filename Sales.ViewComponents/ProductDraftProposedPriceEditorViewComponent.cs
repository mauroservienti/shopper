using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.ViewComponents
{
    [ViewComponent(Name = "Sales.ViewComponents.ProductDraftProposedPriceEditor")]
    public class ProductDraftProposedPriceEditorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic productDraft)
        {
            return View(productDraft.ProposedPrice);
        }
    }
}
