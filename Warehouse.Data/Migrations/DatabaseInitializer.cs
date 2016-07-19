using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Data.Context;
using SQLite.CodeFirst;

namespace Warehouse.Data.Migrations
{
    public class DatabaseInitializer : SqliteCreateDatabaseIfNotExists<WarehouseContext>
    {
        public DatabaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        protected override void Seed(WarehouseContext context)
        {
            context.StockItems.AddOrUpdate(k => k.Id, SeedData.StockItems().ToArray());
        }
    }
}
