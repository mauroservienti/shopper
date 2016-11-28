using CoreViewModelComposition;
using HttpHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Sales.CoreViewModelComposition
{
    public class ProductPriceViewModelVisitor : IProductViewModelVisitor
    {
        readonly IConfiguration _config;

        public ProductPriceViewModelVisitor(IConfiguration config)
        {
            _config = config;
        }

        public async Task Visit(dynamic composedViewModel)
        {
            var apiUrl = _config.GetValue<string>("modules:sales:config:apiUrl");
            var url = $"{apiUrl}ItemPrices/ByStockItem?ids={ composedViewModel.Id }";

            var client = new HttpClient();
            var response = await client.GetAsync(url);
            dynamic[] prices = await response.Content.AsExpandoArrayAsync();

            composedViewModel.ItemPrice = prices.Single();
        }
    }
}
