using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCare.Data.Models;

namespace CustomerCare.Data.Migrations
{
    static class SeedData
    {
        public static Raiting[] Raitings()
        {
            return new Raiting[]
            {
                new Raiting() {StockItemId = 1, Stars = 4.3, ReviewCount=543},
                new Raiting() {StockItemId = 2, Stars = 5, ReviewCount=1234},
                new Raiting() {StockItemId = 3, Stars = 4.2, ReviewCount=1},
                new Raiting() {StockItemId = 4, Stars = .7, ReviewCount=4},
                new Raiting() {StockItemId = 5, Stars = 0, ReviewCount=0}
            };
        }

        public static Review[] Reviews()
        {
            return new Review[]
            {
                new Review()
                {
                    StockItemId = 2,
                    Stars = 4,
                    Author = "Giovanna Coscialunga",
                    Title ="Fondamentale",
                    Text ="fondamentale se non vuoi mangiare marmellata di banana tutte le volte che vai in bicicletta o metti una banana nello zaino"
                }
            };
        }
    }
}
