using CoreViewModelComposition;
using HttpHelpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Marketing.CoreViewModelComposition
{
    public class HomeViewModelBuilder
    {
        IConfiguration _config;
        IEnumerable<IHomeViewModelVisitor> _composers;

        public HomeViewModelBuilder(IConfiguration config, IEnumerable<IHomeViewModelVisitor> composers)
        {
            _config = config;
            _composers = composers;
        }

        public async Task<dynamic> Build()
        {
            var apiUrl = _config.GetValue<string>("modules:marketing:config:apiUrl");

            var client = new HttpClient();
            var response = await client.GetAsync($"{apiUrl}Publishing/GetHomeShowcase");
            dynamic home = await response.Content.AsExpandoAsync();

            dynamic vm = new ExpandoObject();
            vm.HeadlineProduct = new ExpandoObject();
            vm.HeadlineProduct.StockItemId = home.HeadlineStockItemId;
            vm.ShowcaseProducts = new List<dynamic>();

            foreach (var item in home.ShowcaseStockItemIds)
            {
                dynamic obj = new ExpandoObject();
                obj.StockItemId = item;

                vm.ShowcaseProducts.Add(obj);
            }

            var ts = new List<Task>();
            foreach (var composer in _composers)
            {
                ts.Add(composer.Visit(vm));
            }

            await Task.WhenAll(ts.ToArray());

            return vm;
        }
    }
}
