using CustomerCare.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CustomerCare.API.Host.Controllers
{
    [RoutePrefix("api/Reviews")]
    public class ReviewsController : ApiController
    {
        [HttpGet, Route("ByStockItem")]
        public IEnumerable<dynamic> ByStockItem(string ids)
        {
            using (var _context = new CustomerCareContext())
            {
                var _ids = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => int.Parse(id))
                    .ToList();

                var query = from si in _context.Reviews
                            where _ids.Contains(si.StockItemId)
                            select si;

                return query.ToList();
            }
        }
    }
}
