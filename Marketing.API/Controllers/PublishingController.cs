using Marketing.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Marketing.API.Controllers
{
    [RoutePrefix("api/Publishing")]
    public class PublishingController : ApiController
    {
        private readonly IMarketingContext _context;

        public PublishingController(IMarketingContext marketingRepository)
        {
            _context = marketingRepository;
        }

        [HttpGet]
        public dynamic GetHomePageShowcase()
        {
            return null;
        }
    }
}
