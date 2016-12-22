using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.StockItems.Events
{
    public interface IStockItemCreatedEvent
    {
        string StockItemId { get; set; }
    }
}
