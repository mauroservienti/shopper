using System;

namespace Shipping.Data.Models
{
    public class ShippingDetails
    {
        public string Id { get; set; }
        public string StockItemId { get; set; }
        public double Cost { get; set; }
        public decimal Weight { get; set; }
    }
}
