using Marketing.Data.Models;
using Raven.Client;
using System.Dynamic;
using System.Threading.Tasks;

namespace Marketing.Data.Services
{
    public class PublishingService
    {
        IDocumentStore _store;

        public PublishingService(IDocumentStore store)
        {
            _store = store;
        }

        public async Task<dynamic> GetHomeShowcase()
        {
            using (var session = _store.OpenAsyncSession())
            {
                return await session.LoadAsync<HomeStructure>("HomeStructure");
            }
        }
    }
}
