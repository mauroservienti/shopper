using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Data.Models
{
    public class StockItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierDescription { get; set; }
        public decimal Weight { get; set; }
    }
}
