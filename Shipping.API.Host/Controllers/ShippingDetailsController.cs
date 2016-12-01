using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Shipping.Data.Context;
using System;

namespace Shipping.API.Controllers
{
    [RoutePrefix("api/ShippingDetails")]
    public class ShippingDetailsController : ApiController
    {
        [HttpGet]
        public dynamic Get(int id)
        {
            using (ShippingContext _context = new ShippingContext())
            {
                return _context.ShippingDetails.Where(si => si.Id == id).Single();
            }
        }

        [HttpGet, Route("ByStockItem")]
        public IEnumerable<dynamic> ByStockItem(string ids)
        {
            using (ShippingContext _context = new ShippingContext())
            {
                var _ids = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(id => int.Parse(id))
                .ToList();

                var query = from si in _context.ShippingDetails
                            where _ids.Contains(si.StockItemId)
                            select si;

                return query.ToList();
            }
        }
    }
}
