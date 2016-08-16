using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterCatCore.Dto;

namespace ConsoleApplication1
{
    class DbMain : DbContext
    {
        public DbMain():base("TbnProd.Local")
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLoc> ProductLocs { get; set; }
        public DbSet<Company> Companies { get; set; }
        //public DbSet<tmp_ProductAll> tProdAlls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //!!!!

            //Database.SetInitializer<DbMain>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbContext Context
        {
            get
            {
                return this;
            }
        }

        public List<db_table_struct> GetEntityFields(string tablename)
        {
            return Context.Database.SqlQuery<db_table_struct>("select column_name, data_type as column_type " +
                                                              "from INFORMATION_SCHEMA.COLUMNS " +
                                                              "where table_name = @Name and column_name <> @id and column_name <> 'RowVers' and COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA+'.'+ TABLE_NAME),COLUMN_NAME,'IsComputed')  = 0 order by ordinal_position",
                       new SqlParameter("@Name", tablename), new SqlParameter("@id", "id")).ToList();
        }
        public class db_table_struct
        {
            public string column_name { get; set; }
            public string column_type { get; set; }
        }
    }
}
