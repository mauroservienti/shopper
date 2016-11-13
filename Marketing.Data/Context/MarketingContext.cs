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
    //public interface IMarketingContext
    //{
    //    IQueryable<ProductDescription> ProductDescriptions { get; }
    //}

    [DbConfigurationType(typeof(SqLiteConfig))]
    public class MarketingContext : DbContext //, IMarketingContext
    {
        public MarketingContext() : base("Marketing")
        {
        }

        public IDbSet<ProductDescription> ProductDescriptions { get; set; }

        //IQueryable<ProductDescription> IMarketingContext.ProductDescriptions
        //{
        //    get { return this.ProductDescriptions; }
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DatabaseInitializer(modelBuilder));

            modelBuilder.Entity<ProductDescription>()
                .HasKey(sp => sp.Id);
            //.HasMany(e => e.Items)
            //.WithRequired()
            //.HasForeignKey(k => k.OrderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
