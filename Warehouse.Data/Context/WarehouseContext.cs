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
    public interface IWarehouseContext
    {
        IQueryable<StockItem> StockItems { get; }
    }

    [DbConfigurationType(typeof(SqLiteConfig))]
    public class WarehouseContext : DbContext, IWarehouseContext
    {
        public WarehouseContext() : base("Warehouse")
        {
        }

        public IDbSet<StockItem> StockItems { get; set; }

        IQueryable<StockItem> IWarehouseContext.StockItems
        {
            get { return this.StockItems; }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DatabaseInitializer(modelBuilder));

            modelBuilder.Entity<StockItem>()
                .HasKey(sp => sp.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
