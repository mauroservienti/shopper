using Marketing.Data.Models;
using Raven.Client;
using Raven.Client.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Marketing.API.Services
{
    public class ProductsService
    {
        IDocumentStore _store;

        public ProductsService(IDocumentStore store)
        {
            _store = store;
        }

        public async Task<IEnumerable<Product>> GetByStockItem(string[] stockItemIds)
        {
            using (var session = _store.OpenAsyncSession())
            {
                var query = session.Query<Product>()
                    .Where(pd => pd.StockItemId.In(stockItemIds));

                return await query.ToListAsync();
            }
        }

        public async Task<Product> GetById(string id)
        {
            using (var session = _store.OpenAsyncSession())
            {
                return await session.LoadAsync<Product>(id);
            }
        }
    }
}
