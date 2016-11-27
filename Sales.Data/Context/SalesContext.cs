using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sales.Data.Migrations;
using Sales.Data.Models;

namespace Sales.Data.Context
{
    [DbConfigurationType(typeof(SqLiteConfig))]
    public class SalesContext : DbContext
    {
        public SalesContext() : base("Sales")
        {
        }

        public IDbSet<ItemPrice> ItemPrices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DatabaseInitializer(modelBuilder));

            modelBuilder.Entity<ItemPrice>()
                .HasKey(sp => sp.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
