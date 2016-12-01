using Marketing.Data.Models;
using Raven.Client;
using Raven.Client.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketing.Data.Services
{
    public class ProductDescriptionService
    {
        IDocumentStore _store;

        public ProductDescriptionService(IDocumentStore store)
        {
            _store = store;
        }

        public async Task<IEnumerable<Models.ProductDescription>> GetByStockItem(string[] stockItemIds)
        {
            using(var session = _store.OpenAsyncSession())
            {
                var query = session.Query<ProductDescription>()
                    .Where(pd => pd.StockItemId.In(stockItemIds));

                return await query.ToListAsync();
            }
        }
    }
}
