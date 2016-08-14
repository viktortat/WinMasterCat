using System.Data.Entity.Migrations.Model;

namespace WinMasterCat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test4 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TestUsers", newName: "Test12");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TestUser", newName: "Test12");
        }
    }
}
