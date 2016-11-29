using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Warehouse.Data.Context;
using Warehouse.Data.Models;

namespace Warehouse.API.Controllers
{
    [RoutePrefix("api/stockitems")]
    public class StockItemsController : ApiController
    {
        [HttpGet]
        public dynamic Get(int id)
        {
            using (var _repository = new WarehouseContext())
            {
                return _repository.StockItems.Where(si => si.Id == id).Single();
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
    }
}
