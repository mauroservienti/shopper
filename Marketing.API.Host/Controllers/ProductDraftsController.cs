using System;
using System.Linq;
using System.Web.Http;
using Marketing.Data.Context;
using System.Collections.Generic;
using Marketing.Data.Services;
using System.Threading.Tasks;
using Marketing.Data.Models;
using NServiceBus;
using Marketing.API.Host.Commands;

namespace Marketing.API.Controllers
{
    [RoutePrefix("api/ProductDrafts")]
    public class ProductDraftsController : ApiController
    {
        IMessageSession _messageSession;

        public ProductDraftsController( IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            using (var _repository = new MarketingContext())
            {
                //WARN: should apply pagination
                return _repository.ProductDrafts.ToList();
            }
        }

        [HttpPost]
        public async Task<dynamic> Post(ProductDraft model)
        {
            using (var _repository = new MarketingContext())
            {
                _repository.ProductDrafts.Attach(model);
                _repository.Entry(model).State = System.Data.Entity.EntityState.Modified;

                await _repository.SaveChangesAsync();

                await _messageSession.SendLocal<HeyIKnowThisIsWrongButSagaTrustMeDraftIsDoneCommand>(cmd => cmd.StockItemId = model.StockItemId);

                return model.Id;
            }
        }
    }
}
