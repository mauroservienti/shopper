using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.Data.Migrations;
using Shipping.Data.Models;

namespace Shipping.Data.Context
{
    public interface IShippingContext
    {
        IQueryable<ShippingDetails> ShippingDetails { get; }
    }

    [DbConfigurationType(typeof(SqLiteConfig))]
    public class ShippingContext : DbContext, IShippingContext
    {
        public ShippingContext() : base("Shipping")
        {
        }

        public IDbSet<ShippingDetails> ShippingDetails { get; set; }

        IQueryable<ShippingDetails> IShippingContext.ShippingDetails
        {
            get { return this.ShippingDetails; }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DatabaseInitializer(modelBuilder));

            modelBuilder.Entity<ShippingDetails>()
                .HasKey(sp => sp.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
