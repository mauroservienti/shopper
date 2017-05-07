using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.Data.Models;

namespace Shipping.Data.Migrations
{
    public static class SeedData
    {
        public static List<ShippingDetails> ShippingDetails()
        {
            return new List<ShippingDetails>
            {
                new ShippingDetails() {StockItemId = "StockItems/1", Cost = 20, Weight=4},
                new ShippingDetails() {StockItemId = "StockItems/2", Cost = 0, Weight=1},
                new ShippingDetails() {StockItemId = "StockItems/3", Cost = 10, Weight=1},
                new ShippingDetails() {StockItemId = "StockItems/4", Cost = 10, Weight=1},
                new ShippingDetails() {StockItemId = "StockItems/5", Cost = 10, Weight=1}
            };
        }
    }
}
