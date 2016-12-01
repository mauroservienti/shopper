using System.Dynamic;
using System.Threading.Tasks;

namespace Marketing.Data.Services
{
    public class PublishingService
    {
        //IDocumentStore _store;

        //public PublishingService(IDocumentStore store)
        //{
        //    _store = store;
        //}

        public Task<dynamic> GetHomeShowcase()
        {
            dynamic vm = new ExpandoObject();
            vm.HeadlineStockItemId = 1;
            vm.ShowcaseStockItemIds = new[] { 2, 4, 5 };

            return Task.FromResult<dynamic>( vm );
        }
    }
}
