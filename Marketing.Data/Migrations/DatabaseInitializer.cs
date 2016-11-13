using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketing.Data.Context;
using SQLite.CodeFirst;

namespace Marketing.Data.Migrations
{
    public class DatabaseInitializer : SqliteCreateDatabaseIfNotExists<MarketingContext>
    {
        public DatabaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        protected override void Seed(MarketingContext context)
        {
            context.ProductDescriptions.AddOrUpdate(k => k.Id, SeedData.ProductDescriptions().ToArray());
        }
    }
}
