using CoreViewModelComposition;
using HttpHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Shipping.CoreViewModelComposition
{
    public class ShippingInfoHomeViewModelVisitor : IHomeViewModelVisitor
    {
        readonly IConfiguration _config;

        public ShippingInfoHomeViewModelVisitor(IConfiguration config)
        {
            _config = config;
        }

        public async Task Visit(dynamic composedViewModel)
        {
            var ids = new List<dynamic>()
            {
                composedViewModel.HeadlineProduct.StockItemId
            };

            foreach(var p in composedViewModel.ShowcaseProducts)
            {
                ids.Add(p.StockItemId);
            }

            var apiUrl = _config.GetValue<string>("modules:shipping:config:apiUrl");
            var url = $"{apiUrl}ShippingDetails/ByStockItem?ids={ string.Join(",", ids) }";

            var client = new HttpClient();
            var response = await client.GetAsync(url);
            dynamic[] infos = await response.Content.AsExpandoArrayAsync();

            composedViewModel.HeadlineProduct.ItemShippingInfo = infos.Single(d => d.StockItemId == composedViewModel.HeadlineProduct.StockItemId);

            foreach(var p in composedViewModel.ShowcaseProducts)
            {
                var obj = infos.Single(d => d.StockItemId == p.StockItemId);
                p.ItemShippingInfo = obj;
            }
        }
    }
}
