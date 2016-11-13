using CoreViewModelComposition;
using HttpHelpers;
using JsonHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Marketing.CoreViewModelComposition
{
    public class ProductDescriptionHomeComposer : IComposeHomeViewModel
    {
        public async Task Compose(dynamic composedViewModel)
        {
            var ids = new List<int>();
            ids.Add(composedViewModel.HeadlineProduct.StockItemId);
            foreach(var p in composedViewModel.ShowcaseProducts)
            {
                ids.Add(p.StockItemId);
            }
            var apiUrl = $"http://localhost:20188/api/ProductDescriptions/ByStockItem?ids={ string.Join(",", ids) }";

            var client = new HttpClient();
            var response = await client.GetAsync(apiUrl);
            dynamic[] descriptions = await response.Content.AsExpandoArrayAsync();

            composedViewModel.HeadlineProduct.Description = descriptions.Single(d => d.StockItemId == composedViewModel.HeadlineProduct.StockItemId);

            foreach(var p in composedViewModel.ShowcaseProducts)
            {
                var obj = descriptions.Single(d => d.StockItemId == p.StockItemId);
                p.Description = obj;
            }
        }
    }
}
