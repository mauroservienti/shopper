using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCare.Data.Context;
using SQLite.CodeFirst;

namespace CustomerCare.Data.Migrations
{
    public class DatabaseInitializer : SqliteCreateDatabaseIfNotExists<CustomerCareContext>
    {
        public DatabaseInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        protected override void Seed(CustomerCareContext context)
        {
            context.Raitings.AddOrUpdate(k => k.Id, SeedData.Raitings().ToArray());
        }
    }
}
