using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcCoreFrontend.Controllers
{
    public class UnhandledErrorController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
