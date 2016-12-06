using CoreViewModelComposition;
using HttpHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;
using System.Dynamic;

namespace CustomerCare.CoreViewModelComposition
{
    public class ProductViewModelVisitor : IProductViewModelVisitor
    {
        readonly IConfiguration _config;

        public ProductViewModelVisitor(IConfiguration config)
        {
            _config = config;
        }

        public async Task Visit(dynamic composedViewModel)
        {
            var apiUrl = _config.GetValue<string>("modules:customerCare:config:apiUrl");

            var getRatingsUrl = $"{apiUrl}Raitings/ByStockItem?ids={ composedViewModel.Id }";
            var getReviewsUrl = $"{apiUrl}Reviews/ByStockItem?ids={ composedViewModel.Id }";

            //var getRatings = new HttpRequestMessage(
            //    method: HttpMethod.Get,
            //    requestUri: getRatingsUrl);

            //var getReviews = new HttpRequestMessage(
            //    method: HttpMethod.Get,
            //    requestUri: getReviewsUrl);

            //var getRatingsContent = new HttpMessageContent(getRatings);
            //var getReviewsContent = new HttpMessageContent(getReviews);

            //MultipartContent content = new MultipartContent("mixed", "batch_" + Guid.NewGuid().ToString());
            //content.Add(new HttpMessageContent(getRatings));
            //content.Add(new HttpMessageContent(getReviews));

            var client = new HttpClient();
            var tasks = new List<Task>();

            dynamic[] ratings = null;
            dynamic[] reviews = null;

            try
            {
                var getRatingsTask = client.GetAsync(getRatingsUrl);
                tasks.Add(getRatingsTask);

                var getReviewsTask = client.GetAsync(getReviewsUrl);
                tasks.Add(getReviewsTask);

                await Task.WhenAll(tasks);

                ratings = await getRatingsTask.Result.Content.AsExpandoArrayAsync();
                reviews = await getReviewsTask.Result.Content.AsExpandoArrayAsync();
            }
            catch (HttpRequestException)
            {
                ratings = new dynamic[0];
                reviews = new dynamic[0];
            }

            composedViewModel.ItemReviews = reviews;
            if (ratings.Any())
            {
                composedViewModel.ItemRating = ratings.Single();
            }
            else
            {
                dynamic itemRating = new ExpandoObject();
                itemRating.Stars = 0;
                itemRating.StockItemId = composedViewModel.Id;
                composedViewModel.ItemRating = itemRating;
            }
        }
    }
}
