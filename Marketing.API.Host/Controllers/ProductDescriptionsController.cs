using System;
using System.Linq;
using System.Web.Http;
using Marketing.Data.Context;
using System.Collections.Generic;
using Marketing.Data.Services;
using System.Threading.Tasks;

namespace Marketing.API.Controllers
{
    [RoutePrefix("api/ProductDescriptions")]
    public class ProductDescriptionsController : ApiController
    {
        private readonly ProductDescriptionService _service;

        public ProductDescriptionsController(ProductDescriptionService service)
        {
            _service = service;
        }

        //[HttpGet]
        //public dynamic Get(int id)
        //{
        //    return _context.ProductDescriptions.Where(si => si.Id == id).Single();
        //}

        [HttpGet, Route("ByStockItem")]
        public async Task<IEnumerable<dynamic>> ByStockItem(string ids)
        {
            var _ids = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(id => int.Parse(id))
                .ToArray();

            return await _service.GetByStockItem(_ids);
        }
    }
}
