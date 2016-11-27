using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketing.ViewComponents.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index(int id)
        {
            ViewData["Title"] = "Product: " + id;

            return View();
        }
    }
}