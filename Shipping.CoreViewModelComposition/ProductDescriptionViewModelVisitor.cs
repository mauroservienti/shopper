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
    public class ShippingInfoViewModelVisitor : IProductsViewModelVisitor
    {
        readonly IConfiguration _config;

        public ShippingInfoViewModelVisitor(IConfiguration config)
        {
            _config = config;
        }

        public async Task VisitOne(dynamic composedViewModel)
        {
            var apiUrl = _config.GetValue<string>("modules:shipping:config:apiUrl");
            var url = $"{apiUrl}ShippingDetails/ByStockItem?ids={ composedViewModel.StockItemId }";

            var client = new HttpClient();
            var response = await client.GetAsync(url);
            dynamic[] infos = await response.Content.AsExpandoArrayAsync();

            composedViewModel.ItemShippingInfo = infos.Single();
        }
    }
}
