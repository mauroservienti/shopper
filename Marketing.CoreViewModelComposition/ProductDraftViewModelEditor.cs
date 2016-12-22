using CoreViewModelComposition;
using HttpHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Marketing.CoreViewModelComposition
{
    public class ProductDraftViewModelEditor
    {
        IConfiguration _config;
        IEnumerable<IProductDraftViewModelEditor> _editors;

        public ProductDraftViewModelEditor(IConfiguration config, IEnumerable<IProductDraftViewModelEditor> editors)
        {
            _config = config;
            _editors = editors;
        }

        public async Task EditOne( string id, IDictionary<string, StringValues> form)
        {
            var apiUrl = _config.GetValue<string>("modules:marketing:config:apiUrl");

            var client = new HttpClient();

            var obj = new
            {
                Id = id,
                StockItemId = form["ProductDescription_StockItemId"].Single(),
                Title = form["ProductDescription_Title"].Single(),
                Description = form["ProductDescription_Description"].Single()
            };

            var argsAsJson = JsonConvert.SerializeObject(obj);
            var postContent = new StringContent(argsAsJson, Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"{apiUrl}ProductDrafts", postContent);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new System.Exception(result.ReasonPhrase);
            }

            var ts = new List<Task>();
            foreach (var editor in _editors)
            {
                ts.Add(editor.EditOne(id, form));
            }

            await Task.WhenAll(ts.ToArray());
        }
    }
}
