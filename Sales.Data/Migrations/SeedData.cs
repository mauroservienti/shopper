using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sales.Data.Models;

namespace Sales.Data.Migrations
{
    static class SeedData
    {
        public static ItemPrice[] ItemPrices()
        {
            return new ItemPrice[]
            {
                new ItemPrice() {StockItemId = 1, StreetPrice = 10},
                new ItemPrice() {StockItemId = 2, StreetPrice = 300},
                new ItemPrice() {StockItemId = 3, StreetPrice = 22},
                new ItemPrice() {StockItemId = 4, StreetPrice = 55.6},
                new ItemPrice() {StockItemId = 5, StreetPrice = 0.56}
            };
        }
    }
}
