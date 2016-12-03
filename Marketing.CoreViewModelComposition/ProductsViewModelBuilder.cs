using CoreViewModelComposition;
using HttpHelpers;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Marketing.CoreViewModelComposition
{
    public class ProductsViewModelBuilder
    {
        IConfiguration _config;
        IEnumerable<IProductsViewModelVisitor> _composers;

        public ProductsViewModelBuilder(IConfiguration config, IEnumerable<IProductsViewModelVisitor> composers)
        {
            _config = config;
            _composers = composers;
        }

        public async Task<dynamic> BuildOne(string id)
        {
            var apiUrl = _config.GetValue<string>("modules:marketing:config:apiUrl");
            var url = $"{apiUrl}Products?id={ id }";

            var client = new HttpClient();
            var response = await client.GetAsync(url);
            dynamic description = await response.Content.AsExpandoAsync();

            dynamic vm = new ExpandoObject();
            vm.ItemDescription = description;
            vm.StockItemId = description.StockItemId;

            var ts = new List<Task>();
            foreach (var composer in _composers)
            {
                ts.Add(composer.VisitOne(vm));
            }

            await Task.WhenAll(ts.ToArray());

            return vm;
        }
    }
}
