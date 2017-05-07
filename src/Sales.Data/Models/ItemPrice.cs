using System;

namespace Sales.Data.Models
{
    public class ItemPrice
    {
        public string Id { get; set; }
        public string StockItemId { get; set; }
        public double StreetPrice { get; set; }
    }
}
