using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketing.Data.Migrations;
using Marketing.Data.Models;

namespace Marketing.Data.Context
{
    [DbConfigurationType(typeof(SqLiteConfig))]
    public class MarketingContext : DbContext //, IMarketingContext
    {
        public MarketingContext() : base("Marketing")
        {
        }

        public IDbSet<ProductDescription> ProductDescriptions { get; set; }

        public IDbSet<ProductDraft> ProductDrafts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DatabaseInitializer(modelBuilder));

            modelBuilder.Entity<ProductDescription>()
                .HasKey(sp => sp.Id);

            modelBuilder.Entity<ProductDraft>()
                .HasKey(sp => sp.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
