using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreViewModelComposition;

namespace MvcCoreFrontend.Controllers
{
    public class HomeController : Controller
    {
        IHomeViewModelBuilder _builder;

        public HomeController(IHomeViewModelBuilder builder)
        {
            _builder = builder;
        }

        public async Task<IActionResult> Index()
        {
            var vm = await _builder.Build();

            return View(vm);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
