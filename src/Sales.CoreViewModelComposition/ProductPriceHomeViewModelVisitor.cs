using CoreViewModelComposition;
using HttpHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Sales.CoreViewModelComposition
{
    public class ProductPriceHomeViewModelVisitor : IHomeViewModelVisitor
    {
        readonly IConfiguration _config;

        public ProductPriceHomeViewModelVisitor(IConfiguration config)
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

            var apiUrl = _config.GetValue<string>("modules:sales:config:apiUrl");
            var url = $"{apiUrl}ItemPrices/ByStockItem?ids={ string.Join(",", ids) }";

            var client = new HttpClient();
            var response = await client.GetAsync(url);
            dynamic[] prices = await response.Content.AsExpandoArrayAsync();

            composedViewModel.HeadlineProduct.ItemPrice = prices.Single(d => d.StockItemId == composedViewModel.HeadlineProduct.StockItemId);

            foreach(var p in composedViewModel.ShowcaseProducts)
            {
                var obj = prices.Single(d => d.StockItemId == p.StockItemId);
                p.ItemPrice = obj;
            }
        }
    }
}
