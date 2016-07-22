using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCare.Data.Migrations;
using CustomerCare.Data.Models;

namespace CustomerCare.Data.Context
{
    public interface ICustomerCareContext
    {
        IQueryable<Raiting> Raitings { get; }
    }

    [DbConfigurationType(typeof(SqLiteConfig))]
    public class CustomerCareContext : DbContext, ICustomerCareContext
    {
        public CustomerCareContext() : base("CustomerCare")
        {
        }

        public IDbSet<Raiting> Raitings { get; set; }

        IQueryable<Raiting> ICustomerCareContext.Raitings
        {
            get { return this.Raitings; }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DatabaseInitializer(modelBuilder));

            modelBuilder.Entity<Raiting>()
                .HasKey(sp => sp.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
