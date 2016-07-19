using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketing.Data.Models;

namespace Marketing.Data.Migrations
{
    static class SeedData
    {
        public static ProductDescription[] ProductDescriptions()
        {
            return new ProductDescription[]
            {
                new ProductDescription() {
                    StockItemId = 1,
                    Title = "Halloween Pumpkin",
                    Description ="Fantastic Halloween Pumpkin, with a smiling face, white battery led included.",
                    ImageUrl =""},
                new ProductDescription() {StockItemId = 2,
                    Title = "Banana Trolley",
                    Description ="",
                    ImageUrl =""},
                new ProductDescription() {StockItemId = 3,
                    Title = "USB-C Universal power supply",
                    Description ="",
                    ImageUrl =""},
                new ProductDescription() {StockItemId = 4,
                    Title = "Learn ServiceFabric",
                    Description ="Editor:MS-Press|Title:Learn ServiceFabric|Author:Alessandro Melchiori",
                    ImageUrl =""},
                new ProductDescription() {StockItemId = 5,
                    Title = "LH740 Airplane Model",
                    Description ="",
                    ImageUrl =""}
            };
        }
    }
}
