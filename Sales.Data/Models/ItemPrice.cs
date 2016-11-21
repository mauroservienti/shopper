using System;

namespace Sales.Data.Models
{
    public class ItemPrice
    {
        public int Id { get; set; }
        public int StockItemId { get; set; }
        public double StreetPrice { get; set; }
    }
}
