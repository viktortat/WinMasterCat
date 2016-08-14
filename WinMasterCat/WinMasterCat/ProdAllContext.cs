using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinMasterCat
{
    class ProdAllContext : DbContext
    {
        public ProdAllContext():
            base("TbnProd.Local")
        { }

        //public DbSet<ProductProp> ProdProps { get; set; }
        public DbSet<TestUser> Test13 { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<TestUser>().ToTable("Test1");
        }


    }

    [Table("Tabl14")] 
    public class TestUser
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string SName { get; set; }
        public int? Age { get; set; }
    }
    public class ProductProp
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public string PropertyVal { get; set; }
    }
}
