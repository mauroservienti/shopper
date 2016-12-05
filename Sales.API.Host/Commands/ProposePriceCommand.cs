using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.API.Host.Commands
{
    class ProposePriceCommand
    {
        public double ProposedPrice { get; set; }
        public string StockItemId { get; set; }
    }
}
