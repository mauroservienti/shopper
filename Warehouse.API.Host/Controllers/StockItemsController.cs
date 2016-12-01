using NServiceBus;
using Raven.Client;
using Raven.Client.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Warehouse.Data.Models;
using Warehouse.StockItems.Events;

namespace Warehouse.API.Controllers
{
    [RoutePrefix("api/stockitems")]
    public class StockItemsController : ApiController
    {
        IMessageSession _messageSession;
        IDocumentStore _store;

        public StockItemsController(IMessageSession messageSession, IDocumentStore store)
        {
            _messageSession = messageSession;
            _store = store;
        }

        [HttpGet]
        public async Task<dynamic> Get(int id)
        {
            using (var session = _store.OpenAsyncSession())
            {
                return await session.LoadAsync<StockItem>(id);
            }
        }

        [HttpPut]
        public async Task<dynamic> Put(StockItem model)
        {
            using (var session = _store.OpenAsyncSession())
            {
                await session.StoreAsync(model);
                await session.SaveChangesAsync();

                await _messageSession.Publish<IStockItemCreatedEvent>(e => e.StockItemId = model.Id);

                return model.Id;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<dynamic>> Get()
        {
            using (var session = _store.OpenAsyncSession())
            {
                //WARN: should apply pagination
                return await session.Query<StockItem>().ToListAsync();
            }
        }

        [HttpGet, Route("ByStockItem")]
        public async Task<IEnumerable<dynamic>> ByStockItem(string ids)
        {
            using (var session = _store.OpenAsyncSession())
            {
                var _ids = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var query = session.Query<StockItem>().Where(r => r.Id.In(_ids));

                return await query.ToListAsync();
            }
        }

        [HttpGet, Route("Sizings")]
        public async Task<dynamic> Sizings(string id)
        {
            using (var session = _store.OpenAsyncSession())
            {
                var result = await session.LoadAsync<StockItem>(id);

                return new
                {
                    Weight = result.Weight
                };
            }
        }
    }
}
