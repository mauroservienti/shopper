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
    public interface ISalesContext
    {
        IQueryable<ItemPrice> ItemPrices { get; }
    }

    [DbConfigurationType(typeof(SqLiteConfig))]
    public class SalesContext : DbContext, ISalesContext
    {
        public SalesContext() : base("Sales")
        {
        }

        public IDbSet<ItemPrice> SellingPrices { get; set; }

        IQueryable<ItemPrice> ISalesContext.ItemPrices
        {
            get { return this.SellingPrices; }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DatabaseInitializer(modelBuilder));

            modelBuilder.Entity<ItemPrice>()
                .HasKey(sp => sp.Id);
            //.HasMany(e => e.Items)
            //.WithRequired()
            //.HasForeignKey(k => k.OrderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
