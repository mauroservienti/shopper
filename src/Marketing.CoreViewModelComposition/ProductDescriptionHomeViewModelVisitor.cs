using CoreViewModelComposition;
using HttpHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Marketing.CoreViewModelComposition
{
    public class ProductDescriptionHomeViewModelVisitor : IHomeViewModelVisitor
    {
        readonly IConfiguration _config;

        public ProductDescriptionHomeViewModelVisitor(IConfiguration config)
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

            var apiUrl = _config.GetValue<string>("modules:marketing:config:apiUrl");
            var url = $"{apiUrl}Products/ByStockItem?ids={ string.Join(",", ids) }";

            var client = new HttpClient();
            var response = await client.GetAsync(url);
            dynamic[] descriptions = await response.Content.AsExpandoArrayAsync();

            composedViewModel.HeadlineProduct.ItemDescription = descriptions.Single(d => d.StockItemId == composedViewModel.HeadlineProduct.StockItemId);

            foreach(var p in composedViewModel.ShowcaseProducts)
            {
                var obj = descriptions.Single(d => d.StockItemId == p.StockItemId);
                p.ItemDescription = obj;
            }
        }
    }
}
