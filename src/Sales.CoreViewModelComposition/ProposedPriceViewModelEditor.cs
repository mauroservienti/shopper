using CoreViewModelComposition;
using HttpHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using Newtonsoft.Json;
using System.Text;

namespace Sales.CoreViewModelComposition
{
    public class ProposedPriceViewModelEditor : IProductDraftViewModelEditor
    {
        readonly IConfiguration _config;

        public ProposedPriceViewModelEditor(IConfiguration config)
        {
            _config = config;
        }

        public async Task EditOne(string id, IDictionary<string, StringValues> form)
        {
            var apiUrl = _config.GetValue<string>("modules:sales:config:apiUrl");
            var client = new HttpClient();

            var obj = new
            {
                Id = id,
                StockItemId = form["ProposedPrice_StockItemId"].Single(),
                Price = form["ProposedPrice_Price"].Single()
            };

            var argsAsJson = JsonConvert.SerializeObject(obj);
            var postContent = new StringContent(argsAsJson, Encoding.UTF8, "application/json");
            var result = await client.PutAsync($"{apiUrl}ItemPrices/Propose", postContent);
        }
    }
}
