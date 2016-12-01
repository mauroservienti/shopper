using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.Data.Models;

namespace Shipping.Data.Migrations
{
    static class SeedData
    {
        public static ShippingDetails[] ShippingDetails()
        {
            return new ShippingDetails[]
            {
                new ShippingDetails() {StockItemId = 1, Cost = 20, Weight=4},
                new ShippingDetails() {StockItemId = 2, Cost = 0, Weight=1},
                new ShippingDetails() {StockItemId = 3, Cost = 10, Weight=1},
                new ShippingDetails() {StockItemId = 4, Cost = 10, Weight=1},
                new ShippingDetails() {StockItemId = 5, Cost = 10, Weight=1}
            };
        }
    }
}
