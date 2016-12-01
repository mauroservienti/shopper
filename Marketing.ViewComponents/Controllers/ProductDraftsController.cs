using CoreViewModelComposition;
using HttpHelpers;
using Marketing.CoreViewModelComposition;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Marketing.ViewComponents.Controllers
{
    [Route("ProductDrafts")]
    public class ProductDraftsController : Controller
    {
        IEnumerable<IProductDraftsViewModelVisitor> _composers;
        readonly IConfiguration _config;

        public ProductDraftsController(IConfiguration config, IEnumerable<IProductDraftsViewModelVisitor> composers)
        {
            _config = config;
            _composers = composers;
        }

        public async Task<IActionResult> Index()
        {
            var apiUrl = _config.GetValue<string>("modules:marketing:config:apiUrl");

            var client = new HttpClient();
            //WARN: should apply pagination
            var response = await client.GetAsync($"{apiUrl}ProductDrafts");
            dynamic[] productDrafts = await response.Content.AsExpandoArrayAsync();

            var ts = new List<Task>();
            foreach (var composer in _composers)
            {
                ts.Add(composer.Visit(productDrafts));
            }

            await Task.WhenAll(ts.ToArray());

            return View(productDrafts);
        }

        //[Route("{id:int}")]
        //public async Task<IActionResult> Details(int id)
        //{
        //    var vm = await _builder.Build(id);

        //    return View(vm);
        //}
    }
}