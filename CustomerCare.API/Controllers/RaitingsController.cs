using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using CustomerCare.Data.Context;
using NServiceBus;
using System;

namespace CustomerCare.API.Controllers
{
    [RoutePrefix("api/Raitings")]
    public class RaitingsController : ApiController
    {
        private readonly ICustomerCareContext _context;

        public RaitingsController(ICustomerCareContext context)
        {
            _context = context;
        }

        [HttpGet]
        public dynamic Get(int id)
        {
            return _context.Raitings.Where(si => si.Id == id).Single();
        }

        [HttpGet, Route("ByStockItemIds/{ids}")]
        public IEnumerable<dynamic> ByIds(string ids)
        {
            var _ids = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(id => int.Parse(id))
                .ToList();

            var query = from si in _context.Raitings
                        where _ids.Contains(si.StockItemId)
                        select si;

            return query.ToList();
        }
    }
}
