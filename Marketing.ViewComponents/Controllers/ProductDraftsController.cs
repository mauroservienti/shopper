using CoreViewModelComposition;
using HttpHelpers;
using Marketing.CoreViewModelComposition;
using Marketing.ViewComponents.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Marketing.ViewComponents.Controllers
{
    [Route("ProductDrafts")]
    public class ProductDraftsController : Controller
    {
        ProductDraftsViewModelBuilder _builder;

        public ProductDraftsController(ProductDraftsViewModelBuilder builder)
        {
            _builder = builder;
        }

        public async Task<IActionResult> Index()
        {
            var productDrafts = await _builder.BuildAll();

            return View(productDrafts);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _builder.BuildOne($"ProductDrafts/{id}");

            return View(vm);
        }

        //[HttpPost, Route("Edit/{id:int}")]
        //public async Task<IActionResult> Edit(int id, EditedDraft draft)
        //{
        //    return View();
        //}
    }
}