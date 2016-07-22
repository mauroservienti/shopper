using System;

namespace Shipping.Data.Models
{
    public class ShippingDetails
    {
        public int Id { get; set; }
        public int StockItemId { get; set; }
        public double Cost { get; set; }
        public decimal Weight { get; set; }
    }
}
