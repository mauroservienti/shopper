using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Data.Migrations;
using Warehouse.Data.Models;

namespace Warehouse.Data.Context
{
    [DbConfigurationType(typeof(SqLiteConfig))]
    public class WarehouseContext : DbContext
    {
        public WarehouseContext() : base("Warehouse")
        {
        }

        public IDbSet<StockItem> StockItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DatabaseInitializer(modelBuilder));

            modelBuilder.Entity<StockItem>()
                .HasKey(sp => sp.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
