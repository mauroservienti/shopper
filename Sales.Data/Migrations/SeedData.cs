using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sales.Data.Models;

namespace Sales.Data.Migrations
{
    public static class SeedData
    {
        public static List<ItemPrice> ItemPrices()
        {
            return new List<ItemPrice>
            {
                new ItemPrice() {StockItemId = "StockItems/1", StreetPrice = 10},
                new ItemPrice() {StockItemId = "StockItems/2", StreetPrice = 300},
                new ItemPrice() {StockItemId = "StockItems/3", StreetPrice = 22},
                new ItemPrice() {StockItemId = "StockItems/4", StreetPrice = 55.6},
                new ItemPrice() {StockItemId = "StockItems/5", StreetPrice = 0.56}
            };
        }
    }
}
