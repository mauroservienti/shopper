using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Sales.Data.Context;
using System;

namespace Sales.API.Controllers
{
    [RoutePrefix("api/sellingprices")]
    public class SellingPricesController : ApiController
    {
        private readonly ISalesContext _context;

        public SellingPricesController(ISalesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public dynamic Get(int id)
        {
            return _context.SellingPrices.Where(si => si.Id == id).Single();
        }

        [HttpGet, Route("ByStockItemIds/{ids}")]
        public IEnumerable<dynamic> ByIds(string ids)
        {
            var _ids = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(id => int.Parse(id))
                .ToList();

            var query = from si in _context.SellingPrices
                        where _ids.Contains(si.StockItemId)
                        select si;

            return query.ToList();
        }
    }
}
