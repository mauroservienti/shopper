using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System;
using Raven.Client;
using System.Threading.Tasks;
using Raven.Client.Linq;

namespace Shipping.API.Controllers
{
    [RoutePrefix("api/ShippingDetails")]
    public class ShippingDetailsController : ApiController
    {
        IDocumentStore _store;

        public ShippingDetailsController(IDocumentStore store)
        {
            _store = store;
        }

        [HttpGet]
        public async Task<dynamic> Get(string id)
        {
            using (var session = _store.OpenAsyncSession())
            {
                return await session.LoadAsync<Data.Models.ShippingDetails>(id);
            }
        }

        [HttpGet, Route("ByStockItem")]
        public async Task<IEnumerable<dynamic>> ByStockItem(string ids)
        {
            using (var session = _store.OpenAsyncSession())
            {
                var _ids = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var query = session.Query<Data.Models.ShippingDetails>().Where(r => r.StockItemId.In(_ids));

                return await query.ToListAsync();
            }
        }
    }
}
