using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketing.ProductDrafts.Events
{
    public interface IProductDraftReadyToBePricedEvent
    {
        int ProductDraftId { get; set; }
        int StockItemId { get; set; }
    }
}
