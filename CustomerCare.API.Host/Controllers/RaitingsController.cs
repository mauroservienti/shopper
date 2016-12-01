using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System;
using Raven.Client;
using CustomerCare.Data.Models;
using System.Threading.Tasks;
using Raven.Client.Linq;

namespace CustomerCare.API.Controllers
{
    [RoutePrefix("api/Raitings")]
    public class RaitingsController : ApiController
    {
        IDocumentStore _store;

        public RaitingsController( IDocumentStore store )
        {
            _store = store;
        }

        [HttpGet]
        public async Task<dynamic> Get(string id)
        {
            using (var session =  _store.OpenAsyncSession())
            {
                return await session.LoadAsync<Raiting>(id);
            }
        }

        [HttpGet, Route("ByStockItem")]
        public async Task<IEnumerable<dynamic>> ByStockItem(string ids)
        {
            using (var session = _store.OpenAsyncSession())
            {
                var _ids = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var query = session.Query<Raiting>().Where(r => r.StockItemId.In(_ids));

                return await query.ToListAsync();
            }
        }
    }
}
