using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.Data.Context;
using SQLite.CodeFirst;

namespace Shipping.Data.Migrations
{
    public class DatabaseInitializer : SqliteCreateDatabaseIfNotExists<ShippingContext>
    {
        public DatabaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        protected override void Seed(ShippingContext context)
        {
            context.ShippingDetails.AddOrUpdate(k => k.Id, SeedData.ShippingDetails().ToArray());
        }
    }
}
