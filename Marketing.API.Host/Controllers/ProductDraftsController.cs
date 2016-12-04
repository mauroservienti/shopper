using System;
using System.Linq;
using System.Web.Http;
using System.Collections.Generic;
using Marketing.Data.Services;
using System.Threading.Tasks;
using Marketing.Data.Models;
using NServiceBus;
using Marketing.API.Host.Commands;
using Raven.Client;

namespace Marketing.API.Controllers
{
    [RoutePrefix("api/ProductDrafts")]
    public class ProductDraftsController : ApiController
    {
        IMessageSession _messageSession;
        IDocumentStore _store;

        public ProductDraftsController( IMessageSession messageSession, IDocumentStore store)
        {
            _messageSession = messageSession;
            _store = store;
        }

        [HttpGet]
        public async Task<dynamic> Get(string id)
        {
            using (var session = _store.OpenAsyncSession())
            {
                return await session.LoadAsync<ProductDraft>(id);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<dynamic>> Get()
        {
            using (var session = _store.OpenAsyncSession())
            {
                //WARN: should apply pagination
                return await session.Query<ProductDraft>().ToListAsync();
            }
        }

        [HttpPost]
        public async Task<dynamic> Post(dynamic model)
        {
            await _messageSession.SendLocal<HeyIKnowThisIsWrongButSagaTrustMeDraftIsDoneCommand>(cmd =>
            {
                cmd.ProductDraftId = model.Id;
                cmd.StockItemId = model.StockItemId;
                cmd.Title = model.Title;
                cmd.Description = model.Description;
            });

            return model.Id;
        }
    }
}
