using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelComposition;

namespace Marketing.Data.Services
{
    class ProductDescriptionHomeComposer : IComposeHomeViewModel
    {
        ProductDescriptionService _service;
        public ProductDescriptionHomeComposer(ProductDescriptionService service)
        {
            _service = service;
        }

        public async Task Compose(dynamic composedViewModel)
        {
            var ids = new List<int>();
            ids.Add(composedViewModel.HeadlineProduct.StockItemId);
            foreach(var p in composedViewModel.ShowcaseProducts)
            {
                ids.Add(p.StockItemId);
            }

            var descriptions = await _service.GetByStockItem(ids.ToArray());

            composedViewModel.HeadlineProduct.Description = descriptions.Single(d => d.StockItemId == composedViewModel.HeadlineProduct.StockItemId);

            foreach(var p in composedViewModel.ShowcaseProducts)
            {
                var dVm = descriptions.Single(d => d.StockItemId == p.StockItemId);
                p.Description = dVm;
            }
        }
    }
}
