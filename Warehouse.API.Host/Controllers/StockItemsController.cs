using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Warehouse.Data.Context;
using Warehouse.Data.Models;
using Warehouse.StockItems.Events;

namespace Warehouse.API.Controllers
{
    [RoutePrefix("api/stockitems")]
    public class StockItemsController : ApiController
    {
        IMessageSession _messageSession;
        public StockItemsController(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        [HttpGet]
        public dynamic Get(int id)
        {
            using (var _repository = new WarehouseContext())
            {
                return _repository.StockItems.Where(si => si.Id == id).Single();
            }
        }

        [HttpPut]
        public async Task<dynamic> Put(StockItem model)
        {
            using (var _repository = new WarehouseContext())
            {
                _repository.StockItems.Add(model);
                await _repository.SaveChangesAsync();

                await _messageSession.Publish<IStockItemCreatedEvent>(e => e.StockItemId = model.Id);

                return model.Id;
            }
        }

        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            using (var _repository = new WarehouseContext())
            {
                //WARN: should apply pagination
                return _repository.StockItems.ToList();
            }
        }

        [HttpGet, Route("ByIds/{ids}")]
        public IEnumerable<dynamic> ByIds(string ids)
        {
            using (var _repository = new WarehouseContext())
            {
                var _ids = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => int.Parse(id))
                    .ToList();

                var query = from si in _repository.StockItems
                            where _ids.Contains(si.Id)
                            select si;

                return query.ToList();
            }
        }

        [HttpGet, Route("Sizings/{id}")]
        public dynamic Sizings(int id)
        {
            using (var _repository = new WarehouseContext())
            {
                var result = (from si in _repository.StockItems
                            where si.Id==id
                            select si.Weight).Single();

                return new
                {
                    Weight = result
                };
            }
        }
    }
}
