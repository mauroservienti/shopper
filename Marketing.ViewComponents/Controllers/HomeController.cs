using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreViewModelComposition;
using Marketing.CoreViewModelComposition;

namespace MvcCoreFrontend.Controllers
{
    public class HomeController : Controller
    {
        HomeViewModelBuilder _builder;

        public HomeController(HomeViewModelBuilder builder)
        {
            _builder = builder;
        }

        public async Task<IActionResult> Index()
        {
            var vm = await _builder.Build();

            return View(vm);
        }
    }
}
