using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketing.ProductDrafts.Events
{
    public interface IProductDraftReadyToBePricedEvent
    {
        string ProductDraftId { get; set; }
        string StockItemId { get; set; }
    }
}
