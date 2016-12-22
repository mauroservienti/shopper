using CoreViewModelComposition;
using HttpHelpers;
using Marketing.CoreViewModelComposition;
using Marketing.ViewComponents.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketing.ViewComponents.Controllers
{
    [Route("ProductDrafts")]
    public class ProductDraftsController : Controller
    {
        ProductDraftsViewModelBuilder _builder;
        ProductDraftViewModelEditor _editor;

        public ProductDraftsController(ProductDraftsViewModelBuilder builder, ProductDraftViewModelEditor editor)
        {
            _builder = builder;
            _editor = editor;
        }

        public async Task<IActionResult> Index()
        {
            var productDrafts = await _builder.BuildAll();

            return View(productDrafts);
        }

        [Route("PleaseWait")]
        public IActionResult PleaseWait()
        {
            return View();
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _builder.BuildEditableOne($"ProductDrafts/{id}");

            return View(vm);
        }

        [HttpPost, Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, IFormCollection form)
        {
            var dictionary = form.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            await _editor.EditOne($"ProductDrafts/{id}", dictionary);

            return RedirectToAction("PleaseWait");
        }
    }
}