using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreViewModelComposition;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Dynamic;
using HttpHelpers;

namespace MvcCoreFrontend.Controllers
{
    public class HomeController : Controller
    {
        IConfiguration _configuration;
        IEnumerable<IComposeHomeViewModel> _composers;

        public HomeController(IConfiguration configuration, IEnumerable<IComposeHomeViewModel> composers)
        {
            _configuration = configuration;
            _composers = composers;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var config = _configuration.GetSection("app.marketing:config");
                var client = new HttpClient();
                var response = await client.GetAsync($"{config.GetValue<String>("apiUrl")}/Publishing/GetHomeShowcase");
                dynamic home = await response.Content.AsExpandoAsync();
                
                dynamic vm = new ExpandoObject();
                vm.HeadlineProduct = new ExpandoObject();
                vm.HeadlineProduct.StockItemId = (int)home.HeadlineStockItemId;
                vm.ShowcaseProducts = new List<dynamic>();

                foreach(var item in home.ShowcaseStockItemIds)
                {
                    dynamic obj = new ExpandoObject();
                    obj.StockItemId = (int)item;

                    vm.ShowcaseProducts.Add(obj);
                }

                var ts = new List<Task>();
                foreach(var composer in _composers)
                {
                    ts.Add(composer.Compose(vm));
                }

                await Task.WhenAll(ts.ToArray());

                return View(vm);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
