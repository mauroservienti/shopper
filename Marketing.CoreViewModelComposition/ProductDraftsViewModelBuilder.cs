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
    public class ProductDraftsViewModelBuilder
    {
        IConfiguration _config;
        IEnumerable<IProductDraftsViewModelVisitor> _composers;

        public ProductDraftsViewModelBuilder(IConfiguration config, IEnumerable<IProductDraftsViewModelVisitor> composers)
        {
            _config = config;
            _composers = composers;
        }

        public async Task<dynamic> BuildEditableOne(string id)
        {
            var apiUrl = _config.GetValue<string>("modules:marketing:config:apiUrl");

            var client = new HttpClient();
            //WARN: should apply pagination
            var response = await client.GetAsync($"{apiUrl}ProductDrafts?id={id}");
            dynamic productDraft = await response.Content.AsExpandoAsync();

            var ts = new List<Task>();
            foreach (var composer in _composers)
            {
                ts.Add(composer.VisitEditableOne(productDraft));
            }

            await Task.WhenAll(ts.ToArray());

            return productDraft;
        }

        public async Task<IEnumerable<dynamic>> BuildAll()
        {
            var apiUrl = _config.GetValue<string>("modules:marketing:config:apiUrl");

            var client = new HttpClient();
            //WARN: should apply pagination
            var response = await client.GetAsync($"{apiUrl}ProductDrafts");
            dynamic[] productDrafts = await response.Content.AsExpandoArrayAsync();

            var ts = new List<Task>();
            foreach (var composer in _composers)
            {
                ts.Add(composer.VisitAll(productDrafts));
            }

            await Task.WhenAll(ts.ToArray());

            return productDrafts;
        }
    }
}
