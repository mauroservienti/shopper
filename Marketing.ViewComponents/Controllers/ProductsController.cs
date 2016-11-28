using Marketing.CoreViewModelComposition;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Marketing.ViewComponents.Controllers
{
    [Route("Products")]
    public class ProductsController : Controller
    {
        ProductViewModelBuilder _builder;

        public ProductsController(ProductViewModelBuilder builder)
        {
            _builder = builder;
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var vm = await _builder.Build(id);

            return View(vm);
        }
    }
}