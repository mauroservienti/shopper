using CoreViewModelComposition;
using HttpHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Dynamic;

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

            foreach (var p in composedViewModel.ShowcaseProducts)
            {
                ids.Add(p.StockItemId);
            }

            var apiUrl = _config.GetValue<string>("modules:customerCare:config:apiUrl");
            var url = $"{apiUrl}Raitings/ByStockItem?ids={ string.Join(",", ids) }";

            dynamic[] ratings = null;
            var client = new HttpClient();
            try
            {
                var response = await client.GetAsync(url);
                var r = await response.Content.ReadAsStringAsync();
                ratings = await response.Content.AsExpandoArrayAsync();
            }
            catch (HttpRequestException)
            {
                ratings = new dynamic[0];
            }

            dynamic headlineProductItemRating = ratings.SingleOrDefault(d => d.StockItemId == composedViewModel.HeadlineProduct.StockItemId);
            if (headlineProductItemRating == null)
            {
                headlineProductItemRating = new ExpandoObject();
                headlineProductItemRating.Stars = 0;
                headlineProductItemRating.StockItemId = composedViewModel.HeadlineProduct.StockItemId;
            }
            composedViewModel.HeadlineProduct.ItemRating = headlineProductItemRating;

            foreach (var p in composedViewModel.ShowcaseProducts)
            {
                var elementItemRating = ratings.SingleOrDefault(d => d.StockItemId == p.StockItemId);
                if (elementItemRating == null)
                {
                    elementItemRating = new ExpandoObject();
                    elementItemRating.Stars = 0;
                    elementItemRating.StockItemId = p.StockItemId;
                }
                p.ItemRating = elementItemRating;
            }
        }
    }
}
