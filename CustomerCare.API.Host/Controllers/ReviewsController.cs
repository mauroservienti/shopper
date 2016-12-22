using CustomerCare.Data.Models;
using Raven.Client;
using Raven.Client.Linq;
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
        IDocumentStore _store;

        public ReviewsController(IDocumentStore store)
        {
            _store = store;
        }

        [HttpGet, Route("ByStockItem")]
        public async Task<IEnumerable<dynamic>> ByStockItem(string ids)
        {
            using (var session = _store.OpenAsyncSession())
            {
                var _ids = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var query = session.Query<Review>().Where(r => r.StockItemId.In(_ids));

                return await query.ToListAsync();
            }
        }
    }
}
