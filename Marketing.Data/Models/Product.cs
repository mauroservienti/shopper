using System;

namespace Marketing.Data.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string StockItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
