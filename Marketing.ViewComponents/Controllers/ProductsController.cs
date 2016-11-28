using Microsoft.AspNetCore.Mvc;

namespace Marketing.ViewComponents.Controllers
{
    [Route("Products")]
    public class ProductsController : Controller
    {
        [Route("{id:int}")]
        public IActionResult Details(int id)
        {
            ViewData["Title"] = "Product: " + id;

            return View();
        }
    }
}