using Marketing.CoreViewModelComposition;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Marketing.ViewComponents.Controllers
{
    [Route("Products")]
    public class ProductsController : Controller
    {
        ProductsViewModelBuilder _builder;

        public ProductsController(ProductsViewModelBuilder builder)
        {
            _builder = builder;
        }

        [Route("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var vm = await _builder.BuildOne($"Products/{id}");

            return View(vm);
        }
    }
}