using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Shipping.Data.Context;
using NServiceBus;
using System;

namespace Shipping.API.Controllers
{
    [RoutePrefix("api/ShippingDetails")]
    public class ShippingDetailsController : ApiController
    {
        private readonly IShippingContext _context;

        public ShippingDetailsController(IShippingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public dynamic Get(int id)
        {
            return _context.ShippingDetails.Where(si => si.Id == id).Single();
        }

        [HttpGet, Route("ByStockItemIds/{ids}")]
        public IEnumerable<dynamic> ByIds(string ids)
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
