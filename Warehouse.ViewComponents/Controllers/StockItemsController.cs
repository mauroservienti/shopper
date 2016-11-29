using HttpHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Warehouse.ViewComponents.Models;

namespace Warehouse.ViewComponents.Controllers
{
    public class StockItemsController : Controller
    {
        readonly IConfiguration _config;
        public StockItemsController(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var apiUrl = _config.GetValue<string>("modules:warehouse:config:apiUrl");

            var client = new HttpClient();
            //WARN: should apply pagination
            var response = await client.GetAsync($"{apiUrl}Stockitems");
            dynamic[] stockItems = await response.Content.AsExpandoArrayAsync();

            return View(stockItems);
        }

        public IActionResult New()
        {
            return View(new NewStockItem());
        }

        [HttpPost]
        public IActionResult New(NewStockItem stockItem)
        {
            return View();
        }
    }
}