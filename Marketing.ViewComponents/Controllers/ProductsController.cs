using Microsoft.AspNetCore.Mvc;

namespace Marketing.ViewComponents.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Details(int id)
        {
            ViewData["Title"] = "Product: " + id;

            return View();
        }
    }
}