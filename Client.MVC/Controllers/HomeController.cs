using Marketing.Data.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModelComposition;

namespace Client.MVC.Controllers
{
    public class HomeController : Controller
    {
        PublishingService _service;
        IComposeHomeViewModel[] _composers;

        public HomeController(PublishingService service, IComposeHomeViewModel[] composers)
        {
            _service = service;
            _composers = composers;
        }

        public ActionResult Index()
        {
            try
            {
                dynamic home = _service.GetHomeShowcase().GetAwaiter().GetResult();
                //go to publishing and get home structure
                dynamic vm = new ExpandoObject();
                vm.HeadlineProduct = new ExpandoObject();
                //var id = (int)home.HeadlineStockItemId;
                vm.HeadlineProduct.StockItemId = home.HeadlineStockItemId;

                var products = new List<dynamic>();
                foreach(var scp in home.ShowcaseStockItemIds)
                {
                    dynamic scpVm = new ExpandoObject();
                    scpVm.StockItemId = scp;
                    products.Add(scpVm);
                }
                vm.ShowcaseProducts = products.ToArray();

                var ts = new List<Task>();
                foreach(var composer in _composers)
                {
                    var t = composer.Compose(vm);
                    ts.Add(t);
                }

                Task.WhenAll(ts.ToArray());

                return View(vm);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}