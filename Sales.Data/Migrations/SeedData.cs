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
        public static SellingPrice[] SellingPrices()
        {
            return new SellingPrice[]
            {
                new SellingPrice() {StockItemId = 1, Price = 10},
                new SellingPrice() {StockItemId = 2, Price = 300},
                new SellingPrice() {StockItemId = 3, Price = 22},
                new SellingPrice() {StockItemId = 4, Price = 55.6},
                new SellingPrice() {StockItemId = 5, Price = 0.56}
            };
        }
    }
}
