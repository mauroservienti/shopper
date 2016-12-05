using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketing.API.Services;

namespace Marketing.API.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        private readonly ProductsService _service;

        public ProductsController(ProductsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<dynamic> Get(string id)
        {
            return await _service.GetById(id);
        }

        [HttpGet, Route("ByStockItem")]
        public async Task<IEnumerable<dynamic>> ByStockItem(string ids)
        {
            var _ids = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var data = await _service.GetByStockItem(_ids);

            return data;
        }
    }
}
