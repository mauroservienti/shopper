using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System;
using Raven.Client;
using System.Threading.Tasks;
using Sales.Data.Models;
using Raven.Client.Linq;
using NServiceBus;
using Sales.ProposedPrice.Events;
using Sales.API.Host.Commands;

namespace Sales.API.Controllers
{
    [RoutePrefix("api/ItemPrices")]
    public class ItemPricesController : ApiController
    {
        IDocumentStore _store;
        IMessageSession _messageSession;

        public ItemPricesController(IDocumentStore store, IMessageSession messageSession)
        {
            _store = store;
            _messageSession = messageSession;
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

        [HttpPut]
        public async Task<dynamic> Propose(dynamic proposedPrice)
        {
            await _messageSession.SendLocal<ProposePriceCommand>(cmd => 
            {
                cmd.StockItemId = proposedPrice.StockItemId;
                cmd.ProposedPrice = proposedPrice.Price;
            });

            return proposedPrice.StockItemId;
        }
    }
}
