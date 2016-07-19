using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Marketing.Data.Repositories;

namespace Marketing.API.Controllers
{
    [RoutePrefix("api/prices")]
    public class PricingController : ApiController
    {
        private readonly IMarketingRepository _marketingRepository;

        public PricingController(IMarketingRepository marketingRepository)
        {
            _marketingRepository = marketingRepository;
        }

        [HttpGet, Route("total/{productIds}")]
        public async Task<dynamic> GetTotal(string productIds)
        {
            var _ids = productIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                   .Select(id => Guid.Parse(id))
                   .ToList();

            var prices = await _marketingRepository.Prices();

            return _ids.Sum(productId => prices.Single(s => s.ProductId == productId).ItemPrice);
        }
    }
}
