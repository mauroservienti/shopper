using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehouse.ViewComponents.Models
{
    public class NewStockItem
    {
        public string Code { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierDescription { get; set; }
        public decimal Weight { get; set; }
    }
}
