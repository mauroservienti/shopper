using CoreViewModelComposition;
using HttpHelpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Marketing.CoreViewModelComposition
{
    public class ProductDescriptionViewModelVisitor : IProductViewModelVisitor
    {
        readonly IConfiguration _config;

        public ProductDescriptionViewModelVisitor(IConfiguration config)
        {
            _config = config;
        }

        public async Task Visit(dynamic composedViewModel)
        {
            var apiUrl = _config.GetValue<string>("modules:marketing:config:apiUrl");
            var url = $"{apiUrl}ProductDescriptions/ByStockItem?ids={ composedViewModel.Id }";

            var client = new HttpClient();
            var response = await client.GetAsync(url);
            dynamic[] descriptions = await response.Content.AsExpandoArrayAsync();

            composedViewModel.ItemDescription = descriptions.Single();
        }
    }
}
