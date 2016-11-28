using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreFrontend.Controllers
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