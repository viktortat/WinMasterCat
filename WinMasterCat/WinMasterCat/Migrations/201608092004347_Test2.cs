namespace WinMasterCat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TestUsers", "Age", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TestUsers", "Age", c => c.Int(nullable: false));
        }
    }
}
