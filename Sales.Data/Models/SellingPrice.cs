using System;

namespace Sales.Data.Models
{
    public class SellingPrice
    {
        public int Id { get; set; }
        public int StockItemId { get; set; }
        public double Price { get; set; }
    }
}
