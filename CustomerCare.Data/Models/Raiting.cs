using System;

namespace CustomerCare.Data.Models
{
    public class Raiting
    {
        public int Id { get; set; }
        public int StockItemId { get; set; }
        public double Stars { get; set; }
        public int ReviewCount { get; set; }
    }
}
