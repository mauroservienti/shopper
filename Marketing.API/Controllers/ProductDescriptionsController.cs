using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Marketing.Data.Context;
using System.Collections.Generic;

namespace Marketing.API.Controllers
{
    [RoutePrefix("api/ProductDescriptions")]
    public class ProductDescriptionsController : ApiController
    {
        private readonly IMarketingContext _context;

        public ProductDescriptionsController(IMarketingContext marketingRepository)
        {
            _context = marketingRepository;
        }

        [HttpGet]
        public dynamic Get(int id)
        {
            return _context.ProductDescriptions.Where(si => si.Id == id).Single();
        }

        [HttpGet, Route("ByStockItemIds/{ids}")]
        public IEnumerable<dynamic> ByIds(string ids)
        {
            var _ids = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(id => int.Parse(id))
                .ToList();

            var query = from si in _context.ProductDescriptions
                        where _ids.Contains(si.StockItemId)
                        select si;

            return query.ToList();
        }
    }
}
