using System;

namespace Marketing.Data.Models
{
    public class ProductDescription
    {
        public int Id { get; set; }
        public int StockItemId { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String ImageUrl { get; set; }
    }
}
