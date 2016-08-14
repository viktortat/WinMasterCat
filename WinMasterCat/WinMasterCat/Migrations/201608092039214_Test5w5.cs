namespace WinMasterCat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test5w5 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TestUser1", newName: "Tabl14");
            AlterColumn("dbo.Tabl14", "Name", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tabl14", "Name", c => c.String());
            RenameTable(name: "dbo.Tabl14", newName: "TestUser1");
        }
    }
}
