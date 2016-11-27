using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using CustomerCare.Data.Context;
using System;

namespace CustomerCare.API.Controllers
{
    [RoutePrefix("api/Raitings")]
    public class RaitingsController : ApiController
    {
        [HttpGet]
        public dynamic Get(int id)
        {
            using (CustomerCareContext _context = new CustomerCareContext())
            {
                return _context.Raitings.Where(si => si.Id == id).Single();
            }
        }

        [HttpGet, Route("ByStockItem")]
        public IEnumerable<dynamic> ByStockItem(string ids)
        {
            using (CustomerCareContext _context = new CustomerCareContext())
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
}
