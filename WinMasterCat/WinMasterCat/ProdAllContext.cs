using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterCatCore.Dto;

namespace WinMasterCat
{
    class ProdAllContext : DbContext
    {
        public ProdAllContext():
            base("TbnProd.Local")
        { }

        //public DbSet<ProductProp> ProdProps { get; set; }
        public DbSet<TestUser> Test13 { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandLoc> BrandLocs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<TestUser>().ToTable("Test1");
        }
    }
}
