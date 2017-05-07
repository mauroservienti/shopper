namespace Warehouse.Data.Models
{
    public class StockItem
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierDescription { get; set; }
        public decimal Weight { get; set; }
    }
}
