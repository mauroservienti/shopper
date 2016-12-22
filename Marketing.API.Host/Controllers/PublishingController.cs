using Marketing.Data.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace Marketing.API.Controllers
{
    [RoutePrefix("api/Publishing")]
    public class PublishingController : ApiController
    {
        private readonly PublishingService _publishingService;

        public PublishingController(PublishingService publishingService)
        {
            _publishingService = publishingService;
        }

        [HttpGet]
        public Task<dynamic> GetHomeShowcase()
        {
            return _publishingService.GetHomeShowcase();
        }
    }
}
