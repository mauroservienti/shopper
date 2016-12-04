using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.ProposedPrice.Events
{
    public interface IProposedPriceAcceptedEvent
    {
        string StockItemId { get; set; }
    }
}
