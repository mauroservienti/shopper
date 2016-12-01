using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketing.Data.Models
{
    public class ProductDraft
    {
        public int Id { get; set; }
        public int StockItemId { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String ImageUrl { get; set; }
    }
}
