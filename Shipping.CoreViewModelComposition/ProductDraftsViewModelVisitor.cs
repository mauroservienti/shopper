using CoreViewModelComposition;
using HttpHelpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shipping.CoreViewModelComposition
{
    public class ProductDraftsViewModelVisitor : IProductDraftsViewModelVisitor
    {
        readonly IConfiguration _config;

        public ProductDraftsViewModelVisitor(IConfiguration config)
        {
            _config = config;
        }

        public async Task VisitAll(IEnumerable<dynamic> composedViewModels)
        {
            var ids = composedViewModels.Select(vm => vm.StockItemId).ToArray();
            if (ids.Any())
            {
                var apiUrl = _config.GetValue<string>("modules:shipping:config:apiUrl");

                var client = new HttpClient();
                //WARN: should apply pagination
                var response = await client.GetAsync($"{apiUrl}ShippingDetails/ByStockItem?ids={ string.Join(",", ids) }");
                dynamic[] details = await response.Content.AsExpandoArrayAsync();

                foreach (var vm in composedViewModels)
                {
                    var obj = details.Single(d => d.StockItemId == vm.StockItemId);
                    vm.ItemShippingInfo = obj;
                }
            }
        }

        public async Task VisitEditableOne(dynamic composedViewModel)
        {
            var id = composedViewModel.StockItemId;
            var apiUrl = _config.GetValue<string>("modules:shipping:config:apiUrl");

            var client = new HttpClient();
            var response = await client.GetAsync($"{apiUrl}ShippingDetails/ByStockItem?ids={id}");
            dynamic[] details = await response.Content.AsExpandoArrayAsync();
            composedViewModel.ItemShippingInfo = details.Single();
        }
    }
}
