using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System;
using Raven.Client;
using System.Threading.Tasks;
using Sales.Data.Models;
using Raven.Client.Linq;

namespace Sales.API.Controllers
{
    [RoutePrefix("api/ItemPrices")]
    public class ItemPricesController : ApiController
    {
        IDocumentStore _store;

        public ItemPricesController(IDocumentStore store)
        {
            _store = store;
        }

        [HttpGet]
        public async Task<dynamic> Get(string id)
        {
            using (var session = _store.OpenAsyncSession())
            {
                return await session.LoadAsync<ItemPrice>(id);
            }
        }

        [HttpGet, Route("ByStockItem")]
        public async Task<IEnumerable<dynamic>> ByStockItem(string ids)
        {
            using (var session = _store.OpenAsyncSession())
            {
                var _ids = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var query = session.Query<ItemPrice>().Where(r => r.StockItemId.In(_ids));

                return await query.ToListAsync();
            }
        }
    }
}
