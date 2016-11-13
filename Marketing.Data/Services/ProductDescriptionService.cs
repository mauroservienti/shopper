using Marketing.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketing.Data.Services
{
    public class ProductDescriptionService
    {
        //private readonly IMarketingContext _context;

        //public ProductDescriptionService(IMarketingContext marketingRepository)
        //{
        //    _context = marketingRepository;
        //}

        public Task<IEnumerable<Models.ProductDescription>> GetByStockItem(int[] stockItemIds)
        {
            using(var db = new MarketingContext())
            {
                var query = from pd in db.ProductDescriptions
                            where stockItemIds.Contains(pd.StockItemId)
                            select pd;

                return Task.FromResult<IEnumerable<Models.ProductDescription>>(query.ToList());
            }
        }
    }
}
