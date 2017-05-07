using System;

namespace CustomerCare.Data.Models
{
    public class Raiting
    {
        public string Id { get; set; }
        public string StockItemId { get; set; }
        public double Stars { get; set; }
        public int ReviewCount { get; set; }
    }
}
