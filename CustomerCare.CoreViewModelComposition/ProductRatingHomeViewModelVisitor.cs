using CoreViewModelComposition;
using HttpHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CustomerCare.CoreViewModelComposition
{
    public class ProductRatingHomeViewModelVisitor : IHomeViewModelVisitor
    {
        readonly IConfiguration _config;

        public ProductRatingHomeViewModelVisitor(IConfiguration config)
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

            var apiUrl = _config.GetValue<string>("modules:customerCare:config:apiUrl");
            var url = $"{apiUrl}Raitings/ByStockItem?ids={ string.Join(",", ids) }";

            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var r = await response.Content.ReadAsStringAsync();
            dynamic[] ratings = await response.Content.AsExpandoArrayAsync();

            composedViewModel.HeadlineProduct.ItemRating = ratings.Single(d => d.StockItemId == composedViewModel.HeadlineProduct.StockItemId);

            foreach(var p in composedViewModel.ShowcaseProducts)
            {
                var obj = ratings.Single(d => d.StockItemId == p.StockItemId);
                p.ItemRating = obj;
            }
        }
    }
}
