using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Sales.Data.Context;
using System;

namespace Sales.API.Controllers
{
    [RoutePrefix("api/ItemPrices")]
    public class ItemPricesController : ApiController
    {
        [HttpGet]
        public dynamic Get(int id)
        {
            using (SalesContext _context = new SalesContext())
            {
                return _context.ItemPrices.Where(si => si.Id == id).Single();
            }
        }

        [HttpGet, Route("ByStockItem")]
        public IEnumerable<dynamic> ByStockItem(string ids)
        {
            using (SalesContext _context = new SalesContext())
            {
                var _ids = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(id => int.Parse(id))
                .ToList();

                var query = from si in _context.ItemPrices
                            where _ids.Contains(si.StockItemId)
                            select si;

                return query.ToList();
            }
        }
    }
}
