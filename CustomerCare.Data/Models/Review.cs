using System;

namespace CustomerCare.Data.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int StockItemId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Stars { get; set; }
    }
}
